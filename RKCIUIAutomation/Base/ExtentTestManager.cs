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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, TenantName tenantName, TestEnv testEnv, BrowserType browserType, string description = null)
        {
            try
            {
                _parentTest = ExtentManager.Instance
                    .CreateTest(testName.SplitCamelCase(), description);
                ExtentManager.Instance.AddSystemInfo("Tenant", tenantName.ToString());
                ExtentManager.Instance.AddSystemInfo("Environment", testEnv.ToString());
                ExtentManager.Instance.AddSystemInfo("Browser", browserType.ToString());
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateParentTest method : \n{e.Message}");
            }
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, string description = null)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }
    }
}
