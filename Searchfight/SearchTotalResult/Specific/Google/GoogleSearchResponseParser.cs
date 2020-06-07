using System.Text.Json;

namespace Searchfight.SearchTotalResult.Specific.Google
{
    public class GoogleSearchResponseParser : ISearchResponseParser
    {
        public long GetTotalResults(string response)
        {
            var responseObj = JsonSerializer.Deserialize<ResponseObj>(response);

            if (responseObj?.searchInformation == null ||
                !long.TryParse(responseObj.searchInformation.totalResults, out var count))
                throw new SearchResponseParserException(WebSearchEngines.Google, response);

            return count;
        }
    }
}
