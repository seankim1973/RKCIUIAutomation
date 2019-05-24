using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public class DriverOptionsFactory
    {
        [ThreadStatic]
        private DriverOptions Options;

        public DriverOptions DetermineDriverOptions(TestPlatform platform, BrowserType browser, string testDetails)
            => DetermineBrowser(browser, testDetails).DeterminePlatform(platform);
        //{
        //    Options = DetermineBrowser(browser, testDetails);
        //    Options.DeterminePlatform(platform);
        //    return Options;
        //}

        private DriverOptions DetermineBrowser(BrowserType browser, string testDetails)
        {
            if (browser == BrowserType.Chrome)
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddBrowserCapabilities(browser, testDetails);
                Options = chromeOptions;
            }
            else if (browser == BrowserType.Firefox)
            {
                FirefoxOptions ffOptions = new FirefoxOptions();
                ffOptions.AddBrowserCapabilities(browser, testDetails);
                Options = ffOptions;
            }
            else if (browser == BrowserType.MicrosoftEdge || browser == BrowserType.Safari)
            {
                switch (browser)
                {
                    case BrowserType.MicrosoftEdge:
                        Options = new EdgeOptions();
                        break;

                    case BrowserType.Safari:
                        Options = new SafariOptions();
                        break;
                }

                Options.AddBrowserCapabilities(browser, testDetails);
            }
            else
            {
                log.Debug("Unrecognized browser type specified ...defaulting to Chrome");
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddBrowserCapabilities(browser, testDetails);
                Options = chromeOptions;
            }
            return Options;
        }
    }

    internal static class DriverOptionsHelper
    {
        internal static void AddBrowserCapabilities<T>(this T options, BrowserType browser, string testDetails)
        {
            if (browser == BrowserType.Chrome)
            {
                ChromeOptions chromeOptions = BaseUtility.ConvertToType<ChromeOptions>(options);
                chromeOptions.PageLoadStrategy = PageLoadStrategy.None;
                chromeOptions.AddArgument("no-sandbox");
                chromeOptions.AddAdditionalCapability("zal:tz", "America/Chicago", true);
                chromeOptions.AddAdditionalCapability("zal:name", testDetails, true);
                chromeOptions.AddAdditionalCapability("zal:screenResolution", "1600x900", true);
            }
            else if (browser == BrowserType.Firefox)
            {
                FirefoxOptions firefoxOptions = BaseUtility.ConvertToType<FirefoxOptions>(options);
                firefoxOptions.PageLoadStrategy = PageLoadStrategy.None;
                firefoxOptions.AddAdditionalCapability("zal:tz", "America/Chicago", true);
                firefoxOptions.AddAdditionalCapability("zal:name", testDetails, true);
                firefoxOptions.AddAdditionalCapability("zal:screenResolution", "1600x900", true);
            }
            else
            {
                DriverOptions _options = BaseUtility.ConvertToType<DriverOptions>(options);
                _options.PageLoadStrategy = PageLoadStrategy.None;
                _options.AddAdditionalCapability("zal:tz", "America/Chicago");
                _options.AddAdditionalCapability("zal:name", testDetails);
                _options.AddAdditionalCapability("zal:screenResolution", "1600x900");
            }
        }

        internal static DriverOptions DeterminePlatform(this DriverOptions options, TestPlatform platform)
        {
            PlatformType platformType = PlatformType.Linux;

            switch (platform)
            {
                case TestPlatform.Grid:
                    platformType = PlatformType.Linux;
                    break;

                case TestPlatform.GridLocal:
                    platformType = PlatformType.Linux;
                    break;

                case TestPlatform.Linux:
                    platformType = PlatformType.Linux;
                    break;

                case TestPlatform.Windows:
                    platformType = PlatformType.Windows;
                    break;

                case TestPlatform.Mac:
                    platformType = PlatformType.Mac;
                    break;

                case TestPlatform.IOS:
                    platformType = PlatformType.Mac;
                    break;

                case TestPlatform.Android:
                    platformType = PlatformType.Android;
                    break;

                default:
                    log.Debug("Unrecognized Platform... defaulting to Linux");
                    break;
            }

            options.PlatformName = platformType.ToString();
            return options;
        }
    }
}