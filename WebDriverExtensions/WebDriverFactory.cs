﻿using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Firefox;

namespace PecoOnlineScraper.Helpers
{
    public class WebDriverFactory
    {
        public static IWebDriver FirefoxWebDriver()
        {
            return new FirefoxDriver();
        }

        public static IWebDriver PhantomJSWebDriver()
        {
            var service = PhantomJSDriverService.CreateDefaultService();
            service.IgnoreSslErrors = true;
            service.SslProtocol = "any";
            var driver = new PhantomJSDriver(service);
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            return driver;
        }
    }
}
