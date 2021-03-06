﻿
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
        private static readonly string pecoUrl = "http://www.peco-online.ro";

        private readonly NavigationSteps navigationSteps = new NavigationSteps();
        private readonly ResultsSearch resultsSearch = new ResultsSearch();
        private readonly IWebDriver phantomJS; 
        
        public PecoSearch()
        {
            logger.Debug("Start PhantomJS");
            phantomJS = WebDriverFactory.PhantomJSWebDriver();
        }

        public void Start()
        {
            logger.Debug("Navigate to "+ pecoUrl);
            phantomJS.Navigate().GoToUrl(pecoUrl);
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
            logger.Debug("Stop PhantomJS");
            phantomJS.Quit();
        }


    }
}
