
using System.Collections.Generic;
using System.Linq;

using PecoOnlineScraper.Steps;
using PecoOnlineScraper.Results;
using PecoOnlineScraper.Helpers;
using OpenQA.Selenium;

using log4net;

namespace PecoOnlineScraper.Search
{
    public class PecoSearch 
    {

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly NavigationSteps navigationSteps = new NavigationSteps();
        private readonly ResultsSearch resultsSearch = new ResultsSearch();
        private readonly IWebDriver phantomJS; 

        public PecoSearch()
        {
            phantomJS = WebDriverFactory.PhantomJSWebDriver();
            logger.Debug("Start PhantomJS");
        }

        public void Start()
        {
            phantomJS.Navigate().GoToUrl("http://www.peco-online.ro/");
            logger.Debug("Navigate to http://www.peco-online.ro");
        }

        public IDictionary<string, IEnumerable<double>> SearchGplPrice(IEnumerable<string> judete)
        {
            var ret = new Dictionary<string, IEnumerable<double>>();
            foreach(string j in judete)
            {
                navigationSteps.SearchGplJudet(phantomJS, j);
                ret[j] = resultsSearch.RetrieveResults(phantomJS);
                logger.Info(string.Format("{0}: {1}", j, string.Join(",", ret[j].ToArray<double>())));
            }
            return ret;
        }

        public void Close()
        {
            phantomJS.Quit();
            logger.Debug("Stop PhantomJS");
        }


    }
}
