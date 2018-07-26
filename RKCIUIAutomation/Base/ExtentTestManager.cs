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

        public static ExtentTest CreateTestParent(string testName, TenantName tenantName, TestEnv testEnv, string url)
        {
            Console.WriteLine("@@@Entered CreateTestParent method");
            try
            {
                string tenantEnv = $"{tenantName}({testEnv})";
                Console.WriteLine($"@@@Test Environment in CreateTestParent method: {tenantEnv}");
                _parentTest = ExtentManager.Instance
                    .CreateTest(testName.SplitCamelCase(), tenantEnv);
                //ExtentManager.Instance.AddSystemInfo("Tenant", _tenantName);
                //ExtentManager.Instance.AddSystemInfo("Environment", _testEnv);
                //ExtentManager.Instance.AddSystemInfo("Url", url);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateTestParent method : \n{e.Message}");
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
