using System;
using System.Collections.Generic;

using PecoOnlineScraper.Search;

namespace PecoOnlineScraper.Main
{
    class Program
    {

        private static IEnumerable<string> LoadJudete()
        {
            return System.IO.File.ReadLines("lista-judete.txt");
        }

        static void Main(string[] args)
        {
            var listaJudete = LoadJudete();
            PecoSearch search = new PecoSearch();
            try
            {
                search.Start();
                var r = search.SearchGplPrice(listaJudete);
                foreach(string j in listaJudete)
                {
                    Console.Write(j + " => ");
                    foreach(double pret in r[j])
                    {
                        Console.Write(pret + " ");
                    }
                    Console.WriteLine();
                }
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
