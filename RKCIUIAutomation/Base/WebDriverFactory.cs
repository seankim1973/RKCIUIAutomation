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
    public class WebDriverFactory
    {
        protected IWebDriver Driver { get; set; }
        private IWebDriver _Driver;
                
        public static string GridVmIP => gridVmIP;
        private const string gridVmIP = "10.1.1.207";

        protected IWebDriver GetWebDriver(TestPlatform platform, BrowserType browser, string testName)
        {
            _Driver = Driver;
            if (Driver == null)
            {
                if (platform == TestPlatform.Local)
                {
                    switch (browser)
                    {
                        case BrowserType.Chrome:
                            return _Driver = new ChromeDriver();
                        case BrowserType.Firefox:
                            return _Driver = new FirefoxDriver();
                        case BrowserType.MicrosoftEdge:
                            return _Driver = new EdgeDriver();
                        case BrowserType.Safari:
                            return _Driver = new SafariDriver();
                        default:
                            log.Debug("Unrecognized Browser Type... using ChromeDriver");
                            return _Driver = new ChromeDriver();
                    }
                }
                else
                {
                    DesiredCapabilities caps = DetermineCapabilities(platform, browser, testName);
                    return _Driver = new RemoteWebDriver(new Uri($"http://{GridVmIP}:4444/wd/hub"), caps);
                }
            }
            else
                return _Driver;
        }
        
        private DesiredCapabilities Caps { get; set; }
        private DesiredCapabilities DetermineCapabilities(TestPlatform platform, BrowserType browser, string testName)
        {
            if (Caps == null)
            {
                Caps = new DesiredCapabilities();
                DeterminePlatformType(platform);
                DetermineBrowserType(browser);
                Caps.SetCapability("zal:tz", "America/Chicago");
                Caps.SetCapability("zal:name", testName);
                Caps.SetCapability("zal:screenResolution", "1600x900");
                return Caps;
            }
            else
                return Caps;
        }
        private DesiredCapabilities DeterminePlatformType(TestPlatform platform)
        {
            switch (platform)
            {
                case TestPlatform.Grid:
                    Caps.SetCapability(CapabilityType.Platform, "Linux");
                    break;
                case TestPlatform.Linux:
                    Caps.SetCapability(CapabilityType.Platform, "Linux");
                    break;
                case TestPlatform.Windows:
                    Caps.SetCapability(CapabilityType.Platform, "Windows");
                    break;
                case TestPlatform.Mac:
                    Caps.SetCapability(CapabilityType.Platform, "Mac");
                    break;
                case TestPlatform.Android:
                    Caps.SetCapability(CapabilityType.Platform, "Android");
                    break;
                case TestPlatform.IOS:
                    Caps.SetCapability(CapabilityType.Platform, "Mac");
                    break;
                default:
                    log.Debug("Unrecognized Platform... using Linux");
                    Caps.SetCapability(CapabilityType.Platform, "Linux");
                    break;
            }
            return Caps;
        }
        private DesiredCapabilities DetermineBrowserType(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    Caps.SetCapability(CapabilityType.BrowserName, "chrome");
                    break;
                case BrowserType.Firefox:
                    Caps.SetCapability(CapabilityType.BrowserName, "firefox");
                    break;
                case BrowserType.MicrosoftEdge:
                    Caps.SetCapability(CapabilityType.BrowserName, "MicrosoftEdge");
                    break;
                case BrowserType.Safari:
                    Caps.SetCapability(CapabilityType.BrowserName, "safari");
                    break;
                default:
                    log.Debug("Unrecognized browser capabilities... using Chrome");
                    Caps.SetCapability(CapabilityType.BrowserName, "chrome");
                    break;
            }
            return Caps;
        }
    }
}
