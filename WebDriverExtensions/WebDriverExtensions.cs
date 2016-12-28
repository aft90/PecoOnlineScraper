using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PecoOnlineScraper.Helpers
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitForElement(this IWebDriver driver, By by, int timeout)
        {
           return new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(ExpectedConditions.ElementIsVisible(by)); 
        }
    }
}
