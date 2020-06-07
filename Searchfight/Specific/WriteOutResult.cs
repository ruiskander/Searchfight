using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Searchfight.Competition;

namespace Searchfight.Specific
{
    public class WriteOutResult: IWriteOutResult
    {
        public void Write(TextWriter @out, ICompetitionResult results, IEnumerable<string> competitors)
        {
            var engines = (WebSearchEngines[]) Enum.GetValues(typeof(WebSearchEngines));

            foreach (var competitor in competitors)
            {
                var text = $"{competitor}: ";

                text = engines.Aggregate(text, (current, engine) => 
                    current + $"{Enum.GetName(typeof(WebSearchEngines), engine)}: {results.GetResult(engine, competitor)} ");

                @out.WriteLine(text);
            }

            foreach (var engine in engines)
            {
                @out.WriteLine($"{Enum.GetName(typeof(WebSearchEngines), engine)} winner: {results.GetWinner(engine)}");
            }

            @out.WriteLine($"Total winner: {results.GetTotalWinner()}");
        }
    }
}