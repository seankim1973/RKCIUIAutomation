﻿using NUnit.Framework;
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

        public static IWebDriver GetWebDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri = "")
            => FactoryInstance._GetDriver(platform, browser, testDetails, gridUri);

        public static void DismissDriverInstance(IWebDriver driver)
            => FactoryInstance._DismissDriver(driver);

        public static void DismissAllDriverInstances()
            => FactoryInstance._DismissAll();

        private static ICapabilities GetCapabilities(TestPlatform platform, BrowserType browser, string testDetails)
        {
            DriverOptionsFactory DriverOptions = new DriverOptionsFactory();
            return DriverOptions.DetermineDriverOptions(platform, browser, testDetails).ToCapabilities();
        }

        private static ThreadLocal<IWebDriver> driverThread = new ThreadLocal<IWebDriver>();
        private Dictionary<IWebDriver, string> driverToKeyMap = new Dictionary<IWebDriver, string>();

        private IWebDriver _GetDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri = "")
        {
            ICapabilities caps = GetCapabilities(platform, browser, testDetails);
            string newKey = CreateKey(caps, testDetails);
            Console.WriteLine($"_SETDRIVER - NEWKEY: {newKey}");

            if (!driverThread.IsValueCreated)
            {
                CreateNewDriver(platform, browser, testDetails, gridUri);
            }
            else
            {
                IWebDriver currentDriver = driverThread.Value;
                string currentKey = null;

                if (!driverToKeyMap.TryGetValue(currentDriver, out currentKey))
                {
                    Console.WriteLine($"_SETDRIVER - CURRENT KEY: {currentKey}");
                    // The driver was dismissed
                    CreateNewDriver(platform, browser, testDetails, gridUri);
                }
                else
                {
                    if (newKey != currentKey)
                    {
                        // A different flavour of WebDriver is required
                        _DismissDriver(currentDriver);
                        CreateNewDriver(platform, browser, testDetails, gridUri);
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
                            CreateNewDriver(platform, browser, testDetails, gridUri);
                        }
                    }
                }
            }

            Console.WriteLine($"DriverThread Value: {driverThread.Value.ToString()}");
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
            //driverToKeyMap.Clear();
        }

        protected static string CreateKey(ICapabilities capabilities, string testDetails)
        {
            string key = $"{capabilities.ToString()}:{testDetails}";
            Console.WriteLine($"CREATE KEY: {key}");
            return key;
        }

        private void CreateNewDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri = "")
        {
            ICapabilities caps = GetCapabilities(platform, browser, testDetails);
            string newKey = CreateKey(caps, testDetails);

            IWebDriver driver = DetermineWebDriver(platform, browser, testDetails, gridUri);
            driverToKeyMap.Add(driver, newKey);
            driverThread.Value = driver;
        }

        private static IWebDriver DetermineWebDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri)
        {
            ICapabilities caps = GetCapabilities(platform, browser, testDetails);

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
                return new RemoteWebDriver(new Uri($"http://{gridUri}:4444/wd/hub"), caps, TimeSpan.FromMinutes(10));
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