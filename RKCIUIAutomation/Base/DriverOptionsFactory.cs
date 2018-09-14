﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using static RKCIUIAutomation.Base.BaseUtils;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Base
{
    public class DriverOptionsFactory
    {
        protected DriverOptions Options { get; set; }

        public DriverOptions DetermineDriverOptions(TestPlatform platform, BrowserType browser, string testName)
        {            
            Options = DetermineBrowser(browser, testName);
            Options.DeterminePlatform(platform);
            return Options;
        }

        private DriverOptions DetermineBrowser(BrowserType browser, string testName)
        {
            if (browser == BrowserType.Chrome)
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddAdditionalCaps(browser, testName);
                Options = chromeOptions;
            }
            else if (browser == BrowserType.Firefox)
            {
                FirefoxOptions ffOptions = new FirefoxOptions();
                ffOptions.AddAdditionalCaps(browser, testName);
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

                Options.AddAdditionalCaps(browser, testName);
            }
            else
            {
                log.Debug("Unrecognized browser type specified ...defaulting to Chrome");
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddAdditionalCaps(browser, testName);
                Options = chromeOptions;
            }
            return Options;
        }
    }

    
    internal static class DriverOptionsHelper
    {
        internal static PageHelper pageHelper = new PageHelper();

        internal static DriverOptions DeterminePlatform(this DriverOptions options, TestPlatform platform)
        {
            PlatformType platformType = PlatformType.Linux;

            switch (platform)
            {
                case TestPlatform.Grid:
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

        internal static void AddAdditionalCaps<T>(this T options, BrowserType browser, string testName)
        {
            if (browser == BrowserType.Chrome)
            {
                ChromeOptions chromeOptions = pageHelper.ConvertToType<ChromeOptions>(options);
                chromeOptions.AddAdditionalCapability("zal:tz", "America/Chicago", true);
                chromeOptions.AddAdditionalCapability("zal:name", testName, true);
                chromeOptions.AddAdditionalCapability("zal:screenResolution", "1600x900", true);
            }
            else if (browser == BrowserType.Firefox)
            {
                FirefoxOptions firefoxOptions = pageHelper.ConvertToType<FirefoxOptions>(options);
                firefoxOptions.AddAdditionalCapability("zal:tz", "America/Chicago", true);
                firefoxOptions.AddAdditionalCapability("zal:name", testName, true);
                firefoxOptions.AddAdditionalCapability("zal:screenResolution", "1600x900", true);
            }
            else
            {
                DriverOptions _options = pageHelper.ConvertToType<DriverOptions>(options);
                _options.AddAdditionalCapability("zal:tz", "America/Chicago");
                _options.AddAdditionalCapability("zal:name", testName);
                _options.AddAdditionalCapability("zal:screenResolution", "1600x900");
            }
        }
    }
}