using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using RKCIUIAutomation.Config;
using System;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Base
{
    [SetUpFixture]
    [Parallelizable]
    public class WebDriverFactory : DriverOptionsFactory
    {
        protected IWebDriver Driver { get; set; }

        private IWebDriver _Driver = null;

        public static string GridVmIP => gridVmIP;
        private const string gridVmIP = "10.1.1.207";

        protected IWebDriver GetWebDriver(TestPlatform platform, BrowserType browser, string testDetails)
        {
            if (Driver == null)
            {
                if (platform == TestPlatform.Local)
                {
                    _Driver = DetermineLocalDriver(browser);
                }
                else
                {
                    var gridHub = platform == TestPlatform.GridLocal
                        ? "localhost"
                        : GridVmIP;

                    var options = DetermineDriverOptions(platform, browser, testDetails);
                    _Driver = new RemoteWebDriver(new Uri($"http://{gridHub}:4444/wd/hub"), options);

                    //string sessionId = ((RemoteWebDriver)_Driver).SessionId.ToString();
                    //Console.WriteLine($"DRIVER SESSION ID#: {sessionId}");
                }
                Driver = _Driver;
                return Driver;
            }
            else
                return Driver;
        }

        private IWebDriver DetermineLocalDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();

                case BrowserType.Firefox:
                    return new FirefoxDriver();

                case BrowserType.MicrosoftEdge:
                    return new EdgeDriver();

                case BrowserType.Safari:
                    return new SafariDriver();

                default:
                    log.Debug("Unrecognized Browser type specified ... defaulting to ChromeDriver");
                    return new ChromeDriver();
            }
        }
    }
}