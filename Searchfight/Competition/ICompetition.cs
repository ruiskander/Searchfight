using System.IO;
using System.Threading.Tasks;

namespace Searchfight.Competition
{
    public interface ICompetition
    {
        Task Run(string[] args, TextWriter @out);
    }
}