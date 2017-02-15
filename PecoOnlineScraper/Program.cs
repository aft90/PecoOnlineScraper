using System;
using System.Collections.Generic;
using System.Configuration;
using PecoOnlineScraper.Search;
using PecoOnlineScraper.Data;
using PecoOnlineScraper.Save;
using PecoOnlineScraper.Config;
using log4net;


namespace PecoOnlineScraper.Main
{
    class Program
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        private static IEnumerable<string> LoadJudete()
        {
            IJudetSettings js = (dynamic)ConfigurationManager.GetSection("judetSettings");
            return js.Judete;
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
            else
            {
                logger.Warn("No database connection string configured, will not save results");
            }
        }

        static void Main(string[] args)
        {
            using (NDC.Push(String.Format("run-id: {0}", Guid.NewGuid().ToString())))
            {
                try
                {
                    SearchData data = GetResults();
                    SaveResults(data);
                }
                catch (Exception e)
                {
                    logger.Error("Exception occurred", e);
                }
            }
        }
    }
}
