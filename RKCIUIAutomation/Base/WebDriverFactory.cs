using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Base
{
    [SetUpFixture]
    [Parallelizable]
    public class WebDriverFactory : DriverOptionsFactory
    {
        protected IWebDriver Driver { get; set; }

        public WebDriverFactory()
        {
        }

        private static WebDriverFactory instance;

        private static WebDriverFactory FactoryInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebDriverFactory();
                }

                return instance;
            }
        }

        public static IWebDriver GetWebDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridIPv4Hostname = "")
            => FactoryInstance._GetDriver(platform, browser, testDetails, gridIPv4Hostname);

        public static void DismissDriverInstance(IWebDriver driver)
            => FactoryInstance._DismissDriver(driver);

        public static void DismissAllDriverInstances()
            => FactoryInstance._DismissAll();

        [ThreadStatic]
        private ICapabilities capabilities;

        private static ThreadLocal<IWebDriver> driverThread = new ThreadLocal<IWebDriver>();
        private Dictionary<IWebDriver, string> driverToKeyMap = new Dictionary<IWebDriver, string>();

        private IWebDriver _GetDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridIPv4 = "")
        {
            capabilities = DetermineDriverOptions(platform, browser, testDetails).ToCapabilities();

            string newKey = CreateKey(capabilities, gridIPv4);

            if (!driverThread.IsValueCreated)
            {
                CreateNewDriver(platform, browser, testDetails, gridIPv4);
            }
            else
            {
                IWebDriver currentDriver = driverThread.Value;
                if (!driverToKeyMap.TryGetValue(currentDriver, out string currentKey))
                {
                    // The driver was dismissed
                    CreateNewDriver(platform, browser, testDetails, gridIPv4);
                }
                else
                {
                    if (newKey != currentKey)
                    {
                        // A different flavour of WebDriver is required
                        _DismissDriver(currentDriver);
                        CreateNewDriver(platform, browser, testDetails, gridIPv4);
                    }
                    else
                    {
                        // Check the browser is alive
                        try
                        {
                            string currentUrl = currentDriver.Url;
                        }
                        catch (WebDriverException)
                        {
                            CreateNewDriver(platform, browser, testDetails, gridIPv4);
                        }
                    }
                }
            }

            return driverThread.Value;
        }

        /*
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
                    var options = DetermineDriverOptions(platform, browser, testDetails);
                    _Driver = new RemoteWebDriver(new Uri($"http://{GridVmIP}:4444/wd/hub"), options);

                    //string sessionId = ((RemoteWebDriver)_Driver).SessionId.ToString();
                    //Console.WriteLine($"DRIVER SESSION ID#: {sessionId}");
                }
                Driver = _Driver;
                return Driver;
            }
            else
                return Driver;
        }
        */

        private void _DismissDriver(IWebDriver driver)
        {
            if (!driverToKeyMap.ContainsKey(driver))
            {
                throw new Exception($"The driver is not owned by the factory: {driver}");
            }

            if (driver != driverThread.Value)
            {
                throw new Exception("The driver does not belong to the current thread: " + driver);
            }

            driver.Quit();
            driverToKeyMap.Remove(driver);
            driverThread.Dispose();
        }

        private void _DismissAll()
        {
            foreach (IWebDriver driver in new List<IWebDriver>(driverToKeyMap.Keys))
            {
                driver.Quit();
                driverToKeyMap.Remove(driver);
            }
        }

        protected static string CreateKey(ICapabilities capabilities, string gridIP)
            => $"{capabilities.ToString()}:{gridIP}";

        private void CreateNewDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri = "")
        {
            capabilities = DetermineDriverOptions(platform, browser, testDetails).ToCapabilities();
            string newKey = CreateKey(capabilities, gridUri);
            IWebDriver driver = platform == TestPlatform.Local
                ? DetermineLocalDriver(browser)
                : DetermineRemoteDriver(capabilities, gridUri);
            
            driverToKeyMap.Add(driver, newKey);
            driverThread.Value = driver;
        }

        private static IWebDriver DetermineRemoteDriver(ICapabilities capabilities, string gridUri)
            => new RemoteWebDriver(new Uri($"http://{gridUri}:4444/wd/hub"), capabilities, TimeSpan.FromMinutes(10));

        private static IWebDriver DetermineLocalDriver(BrowserType browser)
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