using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;

namespace RKCIUIAutomation.Base
{
    public class BaseClass : WebDriverFactory
    {
        private TestPlatform testPlatform;
        private BrowserType browserType;
        private TestEnv testEnv;
        private Project projectSite;

        [SetUp]
        public void SetUp()
        {
            var _testPlatform = TestContext.Parameters.Get("Platform", "Local");
            var _browserType = TestContext.Parameters.Get("Browser", "Chrome");
            var _testEnv = TestContext.Parameters.Get("Env", "Test");
            var _projectSite = TestContext.Parameters.Get("Project");

            testPlatform = (TestPlatform)Enum.Parse(typeof(TestPlatform), _testPlatform);
            browserType = (BrowserType)Enum.Parse(typeof(BrowserType), _browserType);
            testEnv = (TestEnv)Enum.Parse(typeof(TestEnv), _testEnv);
            projectSite = (Project)Enum.Parse(typeof(Project), _projectSite);

            String siteUrl = DetermineSiteUrl(testEnv, projectSite);

            if (testPlatform == TestPlatform.Local)
            {
                Driver = GetLocalWebDriver(browserType);
                Driver.Navigate().GoToUrl(siteUrl);
            }
            else
            {
                Driver = GetRemoteWebDriver(testPlatform, browserType, siteUrl);
            }            
        }


        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }

    }
}
