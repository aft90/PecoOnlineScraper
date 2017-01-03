using System;
using System.Collections.Generic;
using PecoOnlineScraper.Helpers;
using PecoOnlineScraper.Steps;
using PecoOnlineScraper.Results;
using OpenQA.Selenium;

namespace PecoOnlineScraper.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = WebDriverFactory.PhantomJSWebDriver();
            driver.Navigate().GoToUrl("http://www.peco-online.ro/");
            var listaJudete = new List<string> { "Timis", "Olt", "Teleorman", "Bucuresti", "Bihor", "Salaj", "Cluj" };
            NavigationSteps navigation = new NavigationSteps();
            ResultsSearch search = new ResultsSearch();
            foreach(string judet in listaJudete)
            {
                navigation.SearchGplJudet(driver, judet);
                Console.Write(judet + " => ");
                var results = search.RetrieveResults(driver);
                foreach(var r in results)
                {
                    Console.Write(r + " ");
                }
                Console.WriteLine();
            }
            driver.Quit();
        }
    }
}
