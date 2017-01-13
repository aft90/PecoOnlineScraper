using System;
using System.Collections.Generic;
using System.Configuration;
using PecoOnlineScraper.Search;
using PecoOnlineScraper.Data;
using PecoOnlineScraper.Save;
using log4net;

namespace PecoOnlineScraper.Main
{
    class Program
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        private static IEnumerable<string> LoadJudete()
        {
            return System.IO.File.ReadLines("lista-judete.txt");
        }

        private static SearchData GetResults()
        {
            PecoSearch search = new PecoSearch();
            try
            {
                search.Start();
                return new SearchData(search.SearchGplPrice(LoadJudete()));
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                search.Close();
            }
        }

        private static void SaveResults(SearchData data)
        {
            if(ConfigurationManager.ConnectionStrings["PecoOnline"] != null)
            {
                string cs = ConfigurationManager.ConnectionStrings["PecoOnline"].ConnectionString;
                ResultsSave saver = new ResultsSave(cs);
                saver.SaveResults(data);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                SearchData data = GetResults();
                SaveResults(data);
            }
            catch(Exception e)
            {
                logger.Error("Exception occurred", e);
            }
        }
    }
}
