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
using static RKCIUIAutomation.Base.BaseClass;


namespace RKCIUIAutomation.Base
{
    [SetUpFixture]
    [Parallelizable]
    public class WebDriverFactory : DriverOptionsFactory
    {
        public IWebDriver Driver
        {
            get
            {
                return FactoryInstance._GetDriver(testPlatform, browserType, testDetails, GridVmIP);
            }
            set { }
        }

        public WebDriverFactory()
        {
        }

        [ThreadStatic]
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

        public static void DismissDriverInstance()
            => FactoryInstance._DismissDriver();

        public static void DismissAllDriverInstances()
            => FactoryInstance._DismissAll();

        private static ICapabilities GetCapabilities(TestPlatform platform, BrowserType browser, string testDetails)
        {
            DriverOptionsFactory DriverOptions = new DriverOptionsFactory();
            return DriverOptions.DetermineDriverOptions(platform, browser, testDetails).ToCapabilities();
        }

        private ThreadLocal<IWebDriver> driverThread = new ThreadLocal<IWebDriver>();
        private Dictionary<IWebDriver, string> driverToKeyMap = new Dictionary<IWebDriver, string>();

        private IWebDriver _GetDriver(TestPlatform platform, BrowserType browser, string testDetails, string gridUri = "")
        {
            ICapabilities caps = GetCapabilities(platform, browser, testDetails);
            string newKey = CreateKey(caps, testDetails);
            //Console.WriteLine($"_SETDRIVER - NEWKEY: {newKey}");

            if (!driverThread.IsValueCreated)
            {
                CreateNewDriver(platform, browser, testDetails, gridUri);
            }
            else
            {
                IWebDriver currentDriver = driverThread.Value;

                if (!driverToKeyMap.TryGetValue(currentDriver, out string currentKey))
                {
                    //Console.WriteLine($"_SETDRIVER - CURRENT KEY: {currentKey}");
                    // The driver was dismissed
                    CreateNewDriver(platform, browser, testDetails, gridUri);
                }
                else
                {
                    if (newKey != currentKey)
                    {
                        // A different flavour of WebDriver is required
                        _DismissDriver();
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

            //Console.WriteLine($"DriverThread Value: {driverThread.Value.ToString()}");
            return driverThread.Value;
        }

        private void _DismissDriver()
        {
            IWebDriver currentDriver = driverThread.Value;

            if (!driverToKeyMap.ContainsKey(currentDriver))
            {
                throw new Exception($"The driver is not owned by the factory: {currentDriver}");
            }

            if (currentDriver != driverThread.Value)
            {
                throw new Exception("The driver does not belong to the current thread: " + currentDriver);
            }

            
            Console.WriteLine($"_DISMISSDRIVER - {driverThread.Value}");

            currentDriver.Quit();
            driverToKeyMap.Remove(currentDriver);
            currentDriver.Dispose();
        }

        private void _DismissAll()
        {
            foreach (IWebDriver driver in new List<IWebDriver>(driverToKeyMap.Keys))
            {
                Console.WriteLine($"_DISMISSALLDRIVERs - {driverThread.Value}");
                //driver.Quit();
                driver.Dispose();
                driverToKeyMap.Remove(driver);
            }
            //driverToKeyMap.Clear();
        }

        protected static string CreateKey(ICapabilities capabilities, string testDetails)
        {
            string key = $"{capabilities.ToString()}:{testDetails}";
            //Console.WriteLine($"CREATE KEY: {key}");
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
    }
}