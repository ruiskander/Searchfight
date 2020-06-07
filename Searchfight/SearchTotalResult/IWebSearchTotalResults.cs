using System.Threading.Tasks;

namespace Searchfight.SearchTotalResult
{
    public interface IWebSearchTotalResults
    {
        Task<long> GetTotalResults(string query);
    }
}
