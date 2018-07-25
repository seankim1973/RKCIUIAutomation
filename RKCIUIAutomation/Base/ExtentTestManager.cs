using AventStack.ExtentReports;
using RKCIUIAutomation.Config;
using System;
using System.Runtime.CompilerServices;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Base
{
    public class ExtentTestManager
    {
        private static ExtentTest _parentTest;
        private static ExtentTest _childTest;

        public static ExtentTest CreateTest(string testName, TenantName tenantName, TestEnv testEnv, string url)
        {
            string tenantEnv = $"{tenantName.ToString()}({testEnv.ToString()})";
            try
            {
                _parentTest = ExtentManager.Instance
                    .CreateTest(testName.SplitCamelCase(), tenantEnv);
                ExtentManager.Instance.AddSystemInfo("Tenant", tenantName.ToString());
                ExtentManager.Instance.AddSystemInfo("Environment", testEnv.ToString());
                ExtentManager.Instance.AddSystemInfo("Url", url);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateParentTest method : \n{e.Message}");
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
                log.Debug($"##### Exception occured in CreateTest method : \n{e.Message}");
            }
            return _childTest;
        }

        public static ExtentTest GetTestNode()
        {
            return _childTest;
        }
    }
}
