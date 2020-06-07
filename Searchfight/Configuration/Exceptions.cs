using System;

namespace Searchfight.Configuration
{
    public class SearchEngineConfigurationNotFoundException : Exception
    {
        public SearchEngineConfigurationNotFoundException(WebSearchEngines webSearchEngine)
        {
            WebSearchEngine = webSearchEngine;
        }

        public WebSearchEngines WebSearchEngine { get; }
    }

    public class SearchEngineSettingsNotFoundException : Exception
    {
    }
}
