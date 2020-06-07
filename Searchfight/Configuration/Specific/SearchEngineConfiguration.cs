using System;
using System.Collections.Generic;
using System.Linq;
using Searchfight.WebSearch.Configuration;

namespace Searchfight.Configuration.Specific
{
    public class SearchEngineConfiguration : ISearchEngineConfiguration
    {
        private readonly SearchEngineSettings _searchEngineSettings;

        public SearchEngineConfiguration(WebSearchEngines webSearchEngine, ISearchEnginesConfiguration configuration)
        {
            var searchEnginesSettings = configuration.GetSearchEngineSettings();

            _searchEngineSettings = searchEnginesSettings.FirstOrDefault(
                c => c.Name == Enum.GetName(typeof(WebSearchEngines), webSearchEngine));

            if (_searchEngineSettings == null)
                throw new SearchEngineConfigurationNotFoundException(webSearchEngine);
        }

        public string GetRequestUri(string query)
        {
            var uri = $"{_searchEngineSettings.BaseUri}?q={query}";

            if (_searchEngineSettings.Parameters != null) 
                uri = _searchEngineSettings.Parameters.Aggregate(uri, 
                    (current, parameter) => current + $"&{parameter.Name}={parameter.Value}");

            return uri;
        }

        public IDictionary<string, string> GetRequestHeaders()
        {
            if (_searchEngineSettings.Headers == null)
                return new Dictionary<string, string>();

            IDictionary<string, string> headers = new Dictionary<string, string>();

            foreach (var header in _searchEngineSettings.Headers)
            {
                if (headers.ContainsKey(header.Key)) 
                    continue;

                headers.Add(header.Key, header.Value);
            }

            return headers;
        }
    }
}