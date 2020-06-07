using Searchfight.SearchTotalResult;

namespace Searchfight.Competition.Specific
{
    public class QueryItem
    {
        public QueryItem(WebSearchEngines engineType, IWebSearchTotalResults engine, string query)
        {
            EngineType = engineType;
            Engine = engine;
            Query = query;
        }

        public WebSearchEngines EngineType { get; }
        public IWebSearchTotalResults Engine { get; }
        public string Query { get; }
    }
}