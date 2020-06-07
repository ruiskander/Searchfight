using System.Collections.Generic;
using System.Linq;
using Searchfight.Competition;

namespace Searchfight.Specific
{
    public class CommandLineParser: ICommandLineParser
    {
        public List<string> GetCompetitors(string[] args)
        {
            var competitors = new List<string>();
            string competitor = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith('"'))
                    competitor = arg.Substring(1);
                else if (arg.EndsWith('"'))
                {
                    competitor += " " + arg.Substring(0, arg.Length - 1);
                    competitors.Add(competitor);
                    competitor = null;
                }
                else if (string.IsNullOrEmpty(competitor))
                    competitors.Add(arg);
                else competitor += " " + arg;
            }

            if (competitors.Count < 2)
                throw new CompetitorsLessThanTwoException();

            return competitors.Distinct().ToList();
        }
    }
}
