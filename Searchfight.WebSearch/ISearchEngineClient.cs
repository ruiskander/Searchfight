using System.Threading.Tasks;
using Searchfight.WebSearch.Configuration;

namespace Searchfight.WebSearch
{
    public interface ISearchEngineClient
    {
        Task<string> DoRequest(ISearchEngineConfiguration configuration, string query);
    }
}
