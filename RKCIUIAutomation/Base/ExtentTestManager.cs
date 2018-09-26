using AventStack.ExtentReports;
using RKCIUIAutomation.Config;
using System;
using System.Runtime.CompilerServices;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Base
{
    public static class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest _test;

        [ThreadStatic]
        private static ExtentTest _node;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(this ExtentReports reportInstance, string testCaseNumber, string testName, TenantName tenantName, TestEnv testEnv)
        {
            try
            {
                string tenantEnv = $" - Tenant : {tenantName.ToString()}({testEnv.ToString()})";
                string name = $"{testCaseNumber} : {testName.SplitCamelCase()} {tenantEnv}";

                _test = reportInstance.CreateTest(name);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateTest method : \n{e.Message}");
            }

            return _test;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateNode(this ExtentTest parentTest, string description, string details = null)
        {
            try
            {
                _node = parentTest.CreateNode(description, details);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateNode method : \n{e.Message}");
            }

            return _node;
        }
    }
}