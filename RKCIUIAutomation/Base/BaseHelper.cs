using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RestSharp.Extensions;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public static class BaseHelper
    {
        //private static PageBaseHelper pgbHelper = new PageBaseHelper();

        ///// <summary>
        ///// Returns string value [EnvPrefix_varKey] when varKey argument is provided, otherwise returns string value [EnvPrefix]
        ///// <para>[EnvPrefix] consists of [TestCase Number, Test Name, Test Env, Tenant Name]</para>
        ///// </summary>
        ///// <param name="varKey"></param>
        ///// <returns></returns>
        //public static string GetEnvVarPrefix(string varKey = "")
        //{
        //    string testName = Utility.GetTestName();
        //    string tcNumber = Utility.GetTestCaseNumber();
        //    var prefix = $"{tcNumber}{testName}{BaseClass.testEnv}{BaseClass.tenantName}";
        //    var key = varKey.HasValue()
        //        ? $"{prefix}_{varKey}"
        //        : prefix;

        //    return key;
        //}

        //public static string SplitCamelCase(this string str, bool removeUnderscore = true)
        //{
        //    string value = (removeUnderscore == true) ? Regex.Replace(str, @"_", "") : str;
        //    return Regex.Replace(Regex.Replace(value, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        //}

        //public static string ReplaceSpacesWithUnderscores(this string str)
        //    => Regex.Replace(str, @" ", "_");

        //public static void AssignReportCategories(this ExtentTest testInstance, string[] category)
        //{
        //    for (int i = 0; i < category.Length; i++)
        //    {
        //        testInstance
        //            .AssignCategory(category[i]);
        //    }
        //}

        ///// <summary>
        ///// Allows for test cases to continue running when an error, which is not related to the objective of the test case, occurs but impacts the overall result of the test case.
        ///// Used in conjection with CheckForTestStatusInjection method, which is part of the TearDown attribute in the BaseClass.
        ///// </summary>
        ///// <param name="status"></param>
        ///// <param name="logMsg"></param>
        //public static void InjectTestStatus(TestStatus status, string logMsg)
        //{
        //    CreateVar($"_msgKey", logMsg);
        //    CreateVar($"_statusKey", status.ToString());
        //}

        ///// <summary>
        ///// Used in conjunction with InjectTestStatus method.
        ///// </summary>
        ///// <param name="result"></param>
        ///// <returns></returns>
        //public static List<object> CheckForTestStatusInjection(this TestContext.ResultAdapter result)
        //{
        //    PageHelper pageHelper = new PageHelper();
        //    List<object> testResults = new List<object>();

        //    try
        //    {
        //        TestStatus _testStatus = TestStatus.Inconclusive;

        //        //var prefix = GetEnvVarPrefix();
        //        var injStatusKey = GetEnvVarPrefix("_statusKey");
        //        var injMsgKey = GetEnvVarPrefix("_msgKey");

        //        string injStatus = string.Empty;
        //        string injMsg = string.Empty;

        //        if (HashKeyExists(injStatusKey))
        //        {
        //            injStatus = GetVar(injStatusKey, true);
        //            injMsg = GetVar(injMsgKey, true);

        //            switch (injStatus)
        //            {
        //                case "Warning":
        //                    _testStatus = TestStatus.Warning;
        //                    break;

        //                case "Failed":
        //                    _testStatus = TestStatus.Failed;
        //                    break;

        //                case "Skipped":
        //                    _testStatus = TestStatus.Skipped;
        //                    break;

        //                default:
        //                    _testStatus = TestStatus.Inconclusive;
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            _testStatus = result.Outcome.Status;
        //        }

        //        testResults.Add(_testStatus);
        //        testResults.Add(injMsg);
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(e.StackTrace);
        //    }
            
        //    return testResults;
        //}
    }
}