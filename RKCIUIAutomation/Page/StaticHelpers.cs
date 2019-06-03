using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page
{
    public static class StaticHelpers
    {
        public static string GetString(this Enum value, bool getValue2 = false)
        {
            string output = null;

            try
            {
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                output = (getValue2) ? attrs[0].Value2 : attrs[0].Value;
            }
            catch (Exception)
            {
                throw;
            }
            return output;
        }

        /// <summary>
        /// Returns string value [EnvPrefix_varKey] when varKey argument is provided, otherwise returns string value [EnvPrefix]
        /// <para>[EnvPrefix] consists of [TestCase Number, Test Name, Test Env, Tenant Name]</para>
        /// </summary>
        /// <param name="varKey"></param>
        /// <returns></returns>
        public static string GetEnvVarPrefix(string varKey = "")
        {
            string testName = BaseUtil.GetTestName();
            string tcNumber = BaseUtil.GetTestCaseNumber();
            var prefix = $"{tcNumber}{testName}{BaseClass.testEnv}{BaseClass.tenantName}";
            var key = varKey.HasValue()
                ? $"{prefix}_{varKey}"
                : prefix;

            return key;
        }

        public static string SplitCamelCase(this string str, bool removeUnderscore = true)
        {
            string value = (removeUnderscore == true) ? Regex.Replace(str, @"_", "") : str;
            return Regex.Replace(Regex.Replace(value, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public static string ReplaceSpacesWithUnderscores(this string str)
            => Regex.Replace(str, @" ", "_");

        public static void AssignReportCategories(this ExtentTest testInstance, string[] category)
        {
            for (int i = 0; i < category.Length; i++)
            {
                testInstance
                    .AssignCategory(category[i]);
            }
        }

        /// <summary>
        /// Allows for test cases to continue running when an error, which is not related to the objective of the test case, occurs but impacts the overall result of the test case.
        /// Used in conjection with CheckForTestStatusInjection method, which is part of the TearDown attribute in the BaseClass.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="logMsg"></param>
        public static void InjectTestStatus(TestStatus status, string logMsg)
        {
            BaseUtil.CreateVar($"_msgKey", logMsg);
            BaseUtil.CreateVar($"_statusKey", status.ToString());
        }

        /// <summary>
        /// Used in conjunction with InjectTestStatus method.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<object> CheckForTestStatusInjection(this TestContext.ResultAdapter result)
        {
            List<object> testResults = new List<object>();

            try
            {
                TestStatus _testStatus = TestStatus.Inconclusive;

                //var prefix = GetEnvVarPrefix();
                var injStatusKey = GetEnvVarPrefix("_statusKey");
                var injMsgKey = GetEnvVarPrefix("_msgKey");

                string injStatus = string.Empty;
                string injMsg = string.Empty;

                if (BaseUtil.HashKeyExists(injStatusKey))
                {
                    injStatus = BaseUtil.GetVar(injStatusKey, true);
                    injMsg = BaseUtil.GetVar(injMsgKey, true);

                    switch (injStatus)
                    {
                        case "Warning":
                            _testStatus = TestStatus.Warning;
                            break;

                        case "Failed":
                            _testStatus = TestStatus.Failed;
                            break;

                        case "Skipped":
                            _testStatus = TestStatus.Skipped;
                            break;

                        default:
                            _testStatus = TestStatus.Inconclusive;
                            break;
                    }
                }
                else
                {
                    _testStatus = result.Outcome.Status;
                }

                testResults.Add(_testStatus);
                testResults.Add(injMsg);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return testResults;
        }
    }
}