namespace Searchfight.Competition
{
    public interface ICompetitionResult
    {
        void AddResult(WebSearchEngines searchEngine, string competitor, long result);
        long? GetResult(WebSearchEngines searchEngine, string competitor);
        string GetWinner(WebSearchEngines searchEngine);
        string GetTotalWinner();
    }
}