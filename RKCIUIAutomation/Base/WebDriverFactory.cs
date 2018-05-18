using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using RKCIUIAutomation.Config;
using System;

namespace RKCIUIAutomation
{
    public class WebDriverFactory : ConfigUtil
    {
        public static IWebDriver Driver { get; set; }

        protected IWebDriver GetRemoteWebDriver(TestPlatform platform, BrowserType browser)
        {
            DesiredCapabilities caps = DetermineCapabilities(platform, browser);
            return Driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), caps);
        }

        protected IWebDriver GetLocalWebDriver(BrowserType browser)
        {
            if (browser == BrowserType.Chrome)
            {
                Driver = new ChromeDriver();
            }
            else if (browser == BrowserType.Edge)
            {
                Driver = new EdgeDriver();
            }
            else if (browser == BrowserType.Firefox)
            {
                Driver = new FirefoxDriver();
            }
            else if (browser == BrowserType.Safari)
            {
                Driver = new SafariDriver();
            }
            else
            {
                Console.Out.WriteLine("Local browser type is not implemented.");
            }

            return Driver;
        }

        private DesiredCapabilities caps = null;

        private DesiredCapabilities DetermineCapabilities(TestPlatform platform, BrowserType browser)
        {
            caps = new DesiredCapabilities();
            DeterminePlatformType(platform);
            DetermineBrowserType(browser);
            caps.SetCapability("tz", "America/Chicago");
            return caps;
        }

        private DesiredCapabilities DeterminePlatformType(TestPlatform platform)
        {
            if (platform == TestPlatform.Windows)
            {
                caps.SetCapability(CapabilityType.PlatformName, "Windows");
            }
            else if (platform == TestPlatform.Mac)
            {
                caps.SetCapability(CapabilityType.PlatformName, "Mac");
            }
            else if (platform == TestPlatform.Android)
            {
                caps.SetCapability(CapabilityType.PlatformName, "Android");
            }
            else if (platform == TestPlatform.IOS)
            {
                caps.SetCapability(CapabilityType.PlatformName, "Mac");
            }
            else if (platform == TestPlatform.Linux)
            {
                caps.SetCapability(CapabilityType.PlatformName, "Linux");
            }
            else
            {
                Console.Out.WriteLine("Test Platform is not implemented.");
            }

            return caps;
        }

        private DesiredCapabilities DetermineBrowserType(BrowserType browser)
        {
            if (browser == BrowserType.Chrome)
            {
                caps.SetCapability(CapabilityType.BrowserName, "chrome");
            }
            else if (browser == BrowserType.Edge)
            {
                caps.SetCapability(CapabilityType.BrowserName, "edge");
            }
            else if (browser == BrowserType.Firefox)
            {
                caps.SetCapability(CapabilityType.BrowserName, "firefox");
            }
            else if (browser == BrowserType.Safari)
            {
                caps.SetCapability(CapabilityType.BrowserName, "safari");
            }
            else
            {
                Console.Out.WriteLine("Browser type is not implemented.");
            }

            return caps;
        }
    }
}
