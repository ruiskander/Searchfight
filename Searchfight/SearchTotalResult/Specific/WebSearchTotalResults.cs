using System.Threading.Tasks;
using Searchfight.WebSearch;
using Searchfight.WebSearch.Configuration;

namespace Searchfight.SearchTotalResult.Specific
{
    public class WebSearchTotalResults : IWebSearchTotalResults
    {
        private readonly ISearchEngineConfiguration _configuration;
        private readonly ISearchResponseParser _parser;
        private readonly ISearchEngineClient _searchEngine;

        public WebSearchTotalResults(ISearchEngineConfiguration configuration, ISearchEngineClient searchEngine, ISearchResponseParser parser)
        {
            _configuration = configuration;
            _parser = parser;
            _searchEngine = searchEngine;
        }

        public async Task<long> GetTotalResults(string query)
        {
            var response = await _searchEngine.DoRequest(_configuration, query);

            return _parser.GetTotalResults(response);
        }
    }
}