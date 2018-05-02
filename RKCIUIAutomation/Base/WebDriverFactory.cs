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
        public IWebDriver Driver { get; set; }

        protected IWebDriver GetRemoteWebDriver(TestPlatform platform, BrowserType browser, String siteUrl)
        {
            DesiredCapabilities caps = DetermineCapabilities(platform, browser);
            return Driver = new RemoteWebDriver(new Uri(siteUrl), caps);
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
            caps = DeterminePlatformType(platform);
            caps = DetermineBrowserType(browser);
            return caps;
        }

        private DesiredCapabilities DeterminePlatformType(TestPlatform platform)
        {
            if (platform == TestPlatform.Win10)
            {
                caps.SetCapability("Platform", PlatformType.Windows);
            }
            else if (platform == TestPlatform.Mac)
            {
                caps.SetCapability("Platform", PlatformType.Mac);
            }
            else if (platform == TestPlatform.Android)
            {
                caps.SetCapability("Platform", PlatformType.Android);
            }
            else if (platform == TestPlatform.IOS)
            {
                caps.SetCapability("Platform", PlatformType.Mac);
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
                caps.SetCapability("browserName", "chrome");
            }
            else if (browser == BrowserType.Edge)
            {
                caps.SetCapability("browserName", "edge");
            }
            else if (browser == BrowserType.Firefox)
            {
                caps.SetCapability("browserName", "firefox");
            }
            else if (browser == BrowserType.Safari)
            {
                caps.SetCapability("browserName", "safari");
            }
            else
            {
                Console.Out.WriteLine("Browser type is not implemented.");
            }

            return caps;
        }

    }
}
