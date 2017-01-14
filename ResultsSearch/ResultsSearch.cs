using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

using log4net;

namespace PecoOnlineScraper.Results
{
    public class ResultsSearch
    {

        private readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IWebElement GetResultsTable(IWebDriver driver)
        {
            logger.Debug("Load main results table");
            return driver.FindElement(By.Id("tabelaRezultate"));
        }

        private IEnumerable<IWebElement> GetPricesList(IWebElement table)
        {
            logger.Debug("Load price elements");
            return table.FindElements(By.CssSelector(".pretTD"));
        }

        public IEnumerable<double> RetrieveResults(IWebDriver driver)
        {
            return GetPricesList(GetResultsTable(driver)).Select(webElement => Double.Parse(webElement.Text)).ToList();
        }
    }
}
