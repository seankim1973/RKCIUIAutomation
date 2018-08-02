using AventStack.ExtentReports;
using RKCIUIAutomation.Config;
using System;
using System.Runtime.CompilerServices;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Base
{
    public class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest _test;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testCaseNumber, string testName, string description, TenantName tenantName, TestEnv testEnv)
        {
            try
            {
                string tenantEnv = $" - Tenant : {tenantName.ToString()}({testEnv.ToString()})";
                string name = $"{testCaseNumber} : {testName.SplitCamelCase()} {tenantEnv}<br>{description}";
                _test = ExtentManager.Instance.CreateTest(name);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateTest method : \n{e.Message}");
            }
            return _test;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _test;
        }
    }
}
