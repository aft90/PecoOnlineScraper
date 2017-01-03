using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PecoOnlineScraper.Results
{
    public class ResultsSearch
    {
        private IWebElement GetResultsTable(IWebDriver driver)
        {
            return driver.FindElement(By.Id("tabelaRezultate"));
        }

        private IEnumerable<IWebElement> GetPricesList(IWebElement table)
        {
            return table.FindElements(By.CssSelector(".pretTD"));
        }

        public IEnumerable<double> RetrieveResults(IWebDriver driver)
        {
            return GetPricesList(GetResultsTable(driver)).Select(webElement => Double.Parse(webElement.Text));
        }
    }
}
