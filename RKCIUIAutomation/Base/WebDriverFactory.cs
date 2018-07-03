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
        protected IWebDriver driver { get; set; }
        private IWebDriver Driver;
        
        protected IWebDriver GetWebDriver(TestPlatform platform, BrowserType browser, string testName)
        {
            Driver = driver;
            if (Driver == null)
            {
                if (platform == TestPlatform.Local)
                {
                    switch (browser)
                    {
                        case BrowserType.Chrome:
                            Driver = new ChromeDriver();
                            break;
                        case BrowserType.Firefox:
                            Driver = new FirefoxDriver();
                            break;
                        case BrowserType.Edge:
                            Driver = new EdgeDriver();
                            break;
                        case BrowserType.Safari:
                            Driver = new SafariDriver();
                            break;
                        default:
                            LogDebug("Unrecognized Browser Type... using ChromeDriver");
                            Driver = new ChromeDriver();
                            break;
                    }
                }
                else
                {
                    DesiredCapabilities caps = DetermineCapabilities(platform, browser, testName);
                    Driver = new RemoteWebDriver(new Uri("http://10.1.1.207:4444/wd/hub"), caps);
                }

                return Driver;
            }
            else
                return Driver;
        }
        
        private DesiredCapabilities caps { get; set; }
        private DesiredCapabilities DetermineCapabilities(TestPlatform platform, BrowserType browser, string testName)
        {
            if (caps == null)
            {
                caps = new DesiredCapabilities();
                DeterminePlatformType(platform);
                DetermineBrowserType(browser);
                caps.SetCapability("zal:tz", "America/Chicago");
                caps.SetCapability("zal:name", testName);
                caps.SetCapability("zal:screenResolution", "1440x810");
                return caps;
            }
            else
                return caps;
        }
        private DesiredCapabilities DeterminePlatformType(TestPlatform platform)
        {
            switch (platform)
            {
                case TestPlatform.Linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    break;
                case TestPlatform.Windows:
                    caps.SetCapability(CapabilityType.Platform, "Windows");
                    break;
                case TestPlatform.Mac:
                    caps.SetCapability(CapabilityType.Platform, "Mac");
                    break;
                case TestPlatform.Android:
                    caps.SetCapability(CapabilityType.Platform, "Android");
                    break;
                case TestPlatform.IOS:
                    caps.SetCapability(CapabilityType.Platform, "Mac");
                    break;
                default:
                    LogDebug("Unrecognized Platform... using Linux");
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    break;
            }
            return caps;
        }
        private DesiredCapabilities DetermineBrowserType(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    caps.SetCapability(CapabilityType.BrowserName, "chrome");
                    break;
                case BrowserType.Firefox:
                    caps.SetCapability(CapabilityType.BrowserName, "firefox");
                    break;
                case BrowserType.Edge:
                    caps.SetCapability(CapabilityType.BrowserName, "edge");
                    break;
                case BrowserType.Safari:
                    caps.SetCapability(CapabilityType.BrowserName, "safari");
                    break;
                default:
                    LogDebug("Unrecognized browser capabilities... using Chrome");
                    caps.SetCapability(CapabilityType.BrowserName, "chrome");
                    break;
            }
            return caps;
        }
    }
}
