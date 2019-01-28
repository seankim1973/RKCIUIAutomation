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
        [ThreadStatic]
        public IWebDriver Driver;

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

        private static ThreadLocal<IWebDriver> driverThread = new ThreadLocal<IWebDriver>();
        private Dictionary<IWebDriver, string> driverToKeyMap = new Dictionary<IWebDriver, string>();

        private IWebDriver _GetDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridIPv4 = "")
        {
            IWebDriver currentDriver = null;

            string newKey = CreateKey(testDetails, gridIPv4);
            Console.WriteLine($"_SETDRIVER - NEWKEY: {newKey}");

            if (!driverThread.IsValueCreated)
            {
                CreateNewDriver(platform, browser, testDetails, gridIPv4);
            }
            else
            {
                currentDriver = driverThread.Value;

                if (!driverToKeyMap.TryGetValue(currentDriver, out string currentKey))
                {
                    Console.WriteLine($"_SETDRIVER - CURRENT KEY: {currentKey}");
                    // The driver was dismissed
                    CreateNewDriver(platform, browser, testDetails, gridIPv4);
                }
                else
                {
                    if (!newKey.Equals(currentKey))
                    {
                        // A different flavour of WebDriver is required
                        //_DismissDriver(currentDriver);
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

            driverThread.Value.Dispose();
            driverThread.Value.Close();
            //driverToKeyMap.Remove(driver);          
        }

        private void _DismissAll()
        {
            foreach (IWebDriver driver in new List<IWebDriver>(driverToKeyMap.Keys))
            {
                driverThread.Value.Dispose();
                //driverToKeyMap.Remove(driver);
            }
            driverToKeyMap.Clear();
        }

        protected static string CreateKey(string testDetails, string gridIP)
        {
            string key = $"{testDetails}:{gridIP}";
            Console.WriteLine($"CREATE KEY: {key}");
            return key;
        }

        private void CreateNewDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri = "")
        {
            string newKey = CreateKey(testDetails, gridUri);

            IWebDriver driver = DetermineWebDriver(platform, browser, testDetails, gridUri);
            driverToKeyMap.Add(driver, newKey);
            driverThread.Value = driver;
        }

        private static IWebDriver DetermineWebDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri)
        {
            if (platform == TestPlatform.Local)
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
            else
            {
                DriverOptionsFactory driverOptions = new DriverOptionsFactory();
                ICapabilities capabilities = driverOptions.DetermineDriverOptions(platform, browser, testDetails).ToCapabilities();
                return new RemoteWebDriver(new Uri($"http://{gridUri}:4444/wd/hub"), capabilities, TimeSpan.FromMinutes(10));
            }
        }


        //private static IWebDriver DetermineRemoteDriver(ICapabilities capabilities, string gridUri)
        //    => new RemoteWebDriver(new Uri($"http://{gridUri}:4444/wd/hub"), capabilities, TimeSpan.FromMinutes(10));

        //private static IWebDriver DetermineLocalDriver(BrowserType browser)
        //{
        //    switch (browser)
        //    {
        //        case BrowserType.Chrome:
        //            return new ChromeDriver();

        //        case BrowserType.Firefox:
        //            return new FirefoxDriver();

        //        case BrowserType.MicrosoftEdge:
        //            return new EdgeDriver();

        //        case BrowserType.Safari:
        //            return new SafariDriver();

        //        default:
        //            log.Debug("Unrecognized Browser type specified ... defaulting to ChromeDriver");
        //            return new ChromeDriver();
        //    }
        //}
    }
}