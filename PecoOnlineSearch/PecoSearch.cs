
using System.Collections.Generic;

using PecoOnlineScraper.Steps;
using PecoOnlineScraper.Results;
using PecoOnlineScraper.Helpers;
using OpenQA.Selenium;


namespace PecoOnlineScraper.Search
{
    public class PecoSearch 
    {
        private readonly NavigationSteps navigationSteps = new NavigationSteps();
        private readonly ResultsSearch resultsSearch = new ResultsSearch();
        private readonly IWebDriver phantomJS = WebDriverFactory.PhantomJSWebDriver();

        public void Start()
        {
            phantomJS.Navigate().GoToUrl("http://www.peco-online.ro/");
        }

        public IDictionary<string, IEnumerable<double>> SearchGplPrice(IEnumerable<string> judete)
        {
            var ret = new Dictionary<string, IEnumerable<double>>();
            foreach(string j in judete)
            {
                navigationSteps.SearchGplJudet(phantomJS, j);
                ret[j] = resultsSearch.RetrieveResults(phantomJS);
            }
            return ret;
        }

        public void Close()
        {
            phantomJS.Quit();
        }


    }
}
