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
        private static ExtentTest _parent;
        private static readonly Lazy<ExtentTest> _parentNode = new Lazy<ExtentTest>(() => _parent);
        private static ExtentTest ParentTest { get { return _parentNode.Value; } }

        [ThreadStatic]
        private static ExtentTest _child;
        private static readonly Lazy<ExtentTest> _childNode = new Lazy<ExtentTest>(() => _child);
        private static ExtentTest ChildTest { get { return _childNode.Value; } }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void CreateParentTest(string testClass, string description = null)
        {
            try
            {
                _parent = ExtentManager.Instance.CreateTest(testClass, description);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateParentTest method : \n{e.Message}");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void CreateTest(string testCaseNumber, string testName, string description, TenantName tenantName, TestEnv testEnv)
        {
            try
            {
                string tenantEnv = $" - Tenant : {tenantName.ToString()}({testEnv.ToString()})";
                string name = $"{testCaseNumber} : {testName.SplitCamelCase()} {tenantEnv}<br>{description}";
                _child = _parent.CreateNode(name);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in CreateTest method : \n{e.Message}");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest StartTestReport(string testClass, string testCaseNumber, string testName, string description, TenantName tenantName, TestEnv testEnv)
        {
            try
            {
                string tenantEnv = $" - Tenant : {tenantName.ToString()}({testEnv.ToString()})";
                string name = $"{testCaseNumber} : {testName.SplitCamelCase()} {tenantEnv}<br>{description}";

                _child = ExtentManager.Instance.CreateTest(name);
            }
            catch (Exception e)
            {
                log.Debug($"##### Exception occured in StartTestReport method : \n{e.Message}");
            }
            return GetTest();
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetParentTest() => ParentTest;


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest() => ChildTest;

    }
}
