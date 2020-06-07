using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Searchfight.WebSearch.Configuration;

namespace Searchfight.WebSearch
{
    public class WebSearchEngineClient : ISearchEngineClient
    {

        public async Task<string> DoRequest(ISearchEngineConfiguration configuration, string query)
        {
            var request = HttpWebRequest.Create(configuration.GetRequestUri(query));

            IDictionary<string, string> headers = configuration.GetRequestHeaders();

            if (headers != null)
            {
                foreach (var pair in headers)
                {
                    request.Headers[pair.Key] = pair.Value;
                }
            }

            using var response = await request.GetResponseAsync();
            await using var responseStream = response.GetResponseStream();
            return new StreamReader(responseStream).ReadToEnd();
        }
    }
}