using System.Collections.Generic;
using System.IO;
using Searchfight.Competition;

namespace Searchfight
{
    public interface IWriteOutResult
    {
        void Write(TextWriter @out, ICompetitionResult results, IEnumerable<string> competitors);
    }
}