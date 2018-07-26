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
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        public static ExtentTest CreateTest(string testName, TenantName tenantName, TestEnv testEnv, string url)
        {
            try
            {
                string _tenantName = tenantName.ToString();
                string _testEnv = testEnv.ToString();
                string tenantEnv = $"{_tenantName}({_testEnv})";

                _parentTest = ExtentManager.Instance
                    .CreateTest(testName.SplitCamelCase(), tenantEnv);
                //ExtentManager.Instance.AddSystemInfo("Tenant", _tenantName);
                //ExtentManager.Instance.AddSystemInfo("Environment", _testEnv);
                //ExtentManager.Instance.AddSystemInfo("Url", url);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateTest method : \n{e.Message}");
            }
            return _parentTest;
        }

        public static ExtentTest CreateTestNode(string testName, string description = null)
        {
            try
            {
                _childTest = _parentTest.CreateNode(testName, description);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateTestNode method : \n{e.Message}");
            }
            return _childTest;
        }

        public static ExtentTest GetTestNode()
        {
            return _childTest;
        }
    }
}
