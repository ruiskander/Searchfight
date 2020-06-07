using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Configuration;
using Searchfight.SearchTotalResult;
using Searchfight.Utils;

namespace Searchfight.Competition.Specific
{
    public class Competition: ICompetition
    {
        private readonly IDictionary<WebSearchEngines, IWebSearchTotalResults> _webSearchers;
        private readonly ICompetitionResult _competitionResult;
        private readonly IWriteOutResult _writeOut;
        private readonly ICommandLineParser _commandLineParser;

        public Competition(IDictionary<WebSearchEngines, IWebSearchTotalResults> webSearchers,
            ICompetitionResult competitionResult, IWriteOutResult writeOut, ICommandLineParser commandLineParser)
        {
            _webSearchers = webSearchers;
            _competitionResult = competitionResult;
            _writeOut = writeOut;
            _commandLineParser = commandLineParser;
        }

        public async Task Run(string[] args, TextWriter @out)
        {
            try
            {
                var competitors = _commandLineParser.GetCompetitors(args);
                var queries = GetQueries(competitors);

                await queries.RunWithMaxDegreeOfConcurrency(10,
                    async q =>
                    {
                        _competitionResult.AddResult(q.EngineType, q.Query, await q.Engine.GetTotalResults(q.Query));
                    });

                _writeOut.Write(@out, _competitionResult, competitors);
            }
            catch (CompetitorsLessThanTwoException)
            {
                @out.WriteLine("Please, set at least two competitors.");
                @out.WriteLine("Usage: Searchfight.exe .net \"java script\" python");
            }
            catch (SearchEngineSettingsNotFoundException)
            {
                @out.WriteLine("Can't find search engine settings");
            }
            catch (SearchEngineConfigurationNotFoundException e)
            {
                @out.WriteLine($"Can't find configuration for {Enum.GetName(typeof(WeakReference), e.WebSearchEngine)} search engine");
            }
            catch (SearchResponseParserException e)
            {
                @out.WriteLine($"Can't parse response from {Enum.GetName(typeof(WeakReference), e.WebSearchEngine)} search engine");
            }
        }

        private List<QueryItem> GetQueries(IList<string> competitors)
        {
            var queries = new List<QueryItem>();

            foreach (var webSearcher in _webSearchers)
            {
                queries.AddRange(competitors.Select(competitor =>
                    new QueryItem(webSearcher.Key, webSearcher.Value, competitor)));
            }

            return queries;
        }
    }
}