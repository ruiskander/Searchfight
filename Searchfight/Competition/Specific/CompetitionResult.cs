using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Searchfight.Competition.Specific
{
    public class CompetitionResult: ICompetitionResult
    {
        private readonly IDictionary<WebSearchEngines, IDictionary<string, long>> _result;

        public CompetitionResult()
        {
            _result = new ConcurrentDictionary<WebSearchEngines, IDictionary<string, long>>();
            foreach (WebSearchEngines engine in (WebSearchEngines[]) Enum.GetValues(typeof(WebSearchEngines)))
            {
                _result.Add(engine, new ConcurrentDictionary<string, long>());
            }
        }

        public void AddResult(WebSearchEngines searchEngine, string competitor, long result)
        {
            if (_result[searchEngine].ContainsKey(competitor))
                _result[searchEngine][competitor] = result;
            else
                _result[searchEngine].Add(competitor, result);
        }

        public long? GetResult(WebSearchEngines searchEngine, string competitor)
        {
            if (!_result.ContainsKey(searchEngine)) 
                return null;

            if (_result[searchEngine].ContainsKey(competitor))
            {
                return _result[searchEngine][competitor];
            }

            return null;
        }

        public string GetWinner(WebSearchEngines searchEngine)
        {
            if (_result.ContainsKey(searchEngine))
            {
                long max = 0;
                string winner = null;
                foreach (var p in _result[searchEngine])
                {
                    if (p.Value < max)
                        continue;

                    max = p.Value;
                    winner = p.Key;
                }

                return winner;
            }

            return null;
        }

        public string GetTotalWinner()
        {
            long max = 0;
            string winner = null;

            foreach (var v in _result.Values)
            {
                foreach (var p in v)
                {
                    if (p.Value < max)
                        continue;

                    max = p.Value;
                    winner = p.Key;
                }
            }

            return winner;
        }
    }
}