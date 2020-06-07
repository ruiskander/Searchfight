using System.Collections.Generic;

namespace Searchfight
{
    public interface ICommandLineParser
    {
        List<string> GetCompetitors(string[] args);
    }
}