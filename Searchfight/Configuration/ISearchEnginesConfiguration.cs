using System.Collections.Generic;

namespace Searchfight.Configuration
{
    public interface ISearchEnginesConfiguration
    {
        IEnumerable<SearchEngineSettings> GetSearchEngineSettings();
    }
}