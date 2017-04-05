using NUnit.Framework;
using PecoOnlineScraper.Search;
using System.Collections.Generic;
using System.Linq;

namespace QuickScrapeTest
{
    [TestFixture]
    public class QuickScrapeTest
    {
        private PecoSearch search;

        [OneTimeSetUp]
        public void SetUp()
        {
            search = new PecoSearch();
            search.Start();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            search.Close();
        }

        [Test]
        public void TestJudet()
        {
            string judet = "Covasna";
            string[] judetList = { judet };
            var result = search.SearchGplPrice(new List<string>(judetList));
            Assert.AreEqual(2.34, result[judet].ElementAt(0), 0.01);
        }
    }
}
