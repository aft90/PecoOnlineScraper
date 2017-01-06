using System;

namespace PecoOnlineScraper.Data
{
    public class SearchMetadata
    {
        private SearchMetadata(DateTime searchTime)
        {
            SearchTime = searchTime;
        }
        public DateTime SearchTime { get; private set; }

        internal static SearchMetadata NewInstance()
        {
            return new SearchMetadata(DateTime.Now);
        }
    }
}
