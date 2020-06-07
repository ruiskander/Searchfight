using System.Collections.Generic;

namespace Searchfight.WebSearch.Configuration
{
    public interface ISearchEngineConfiguration
    {
        string GetRequestUri(string query);
        IDictionary<string, string> GetRequestHeaders();
    }
}