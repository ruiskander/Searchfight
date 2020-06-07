using System.Text.Json;

namespace Searchfight.SearchTotalResult.Specific.Bing
{
    public class BingSearchResponseParser : ISearchResponseParser
    {
        public long GetTotalResults(string response)
        {
            var responseObj = JsonSerializer.Deserialize<ResponseObj>(response);

            if (responseObj?.webPages == null)
                throw new SearchResponseParserException(WebSearchEngines.Bing, response);

            return responseObj.webPages.totalEstimatedMatches;
        }
    }
}
