using System;
using System.Collections.Generic;

using PecoOnlineScraper.Search;

namespace PecoOnlineScraper.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var listaJudete = new List<string> { "Timis", "Olt", "Teleorman", "Bucuresti", "Bihor", "Salaj", "Cluj" };
            PecoSearch search = new PecoSearch();
            try
            {
                search.Start();
                var r = search.SearchGplPrice(listaJudete);
                foreach (var i in r["Bucuresti"]) Console.WriteLine(i);
                search.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                search.Close();
            }
            
            
        }
    }
}
