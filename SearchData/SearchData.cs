using System.Collections.Generic;

namespace PecoOnlineScraper.Data
{
    public class SearchData
    {
        public SearchData(IDictionary<string, IEnumerable<double>> judetResults)
        {
            Metadata = SearchMetadata.NewInstance();
            JudetResults = judetResults;
        }

        public SearchMetadata Metadata { get; private set;  }
        public IDictionary<string, IEnumerable<double>> JudetResults { get; private set; }
    }
}
