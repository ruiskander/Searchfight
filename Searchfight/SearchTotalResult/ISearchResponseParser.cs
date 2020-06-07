namespace Searchfight.SearchTotalResult
{
    public interface ISearchResponseParser
    {
        long GetTotalResults(string response);
    }
}