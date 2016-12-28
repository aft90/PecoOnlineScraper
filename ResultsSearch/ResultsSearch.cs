using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PecoOnlineScraper.Helpers;
namespace PecoOnlineScraper.Results
{
    public class ResultsSearch
    {
        private IWebElement GetResultsTable(IWebDriver driver)
        {
            return driver.WaitForElement(By.Id("tabelaRezultate"), 20);
        }

        public IReadOnlyCollection<double> RetrieveResults(IWebDriver driver)
        {
            List<double> results = new List<double>();
            IWebElement resultsTable = GetResultsTable(driver);
            var r = resultsTable.FindElements(By.CssSelector(".pretTD"));
            foreach(var e in r)
            {
                results.Add(Double.Parse(e.Text));
            }
            return results;
        }
    }
}
