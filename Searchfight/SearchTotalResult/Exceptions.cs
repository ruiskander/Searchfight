using System;

namespace Searchfight.SearchTotalResult
{
    public class SearchResponseParserException : Exception
    {
        public SearchResponseParserException(WebSearchEngines webSearchEngine, string response)
        {
            WebSearchEngine = webSearchEngine;
            Response = response;
        }

        public WebSearchEngines WebSearchEngine { get; }
        public string Response { get; }
    }
}
