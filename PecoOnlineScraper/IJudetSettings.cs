using System.Collections.Generic;

namespace PecoOnlineScraper.Config
{
    public interface IJudetSettings
    {
        IEnumerable<string> Judete { get; }
    }
}
