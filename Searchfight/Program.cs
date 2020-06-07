using System;
using Microsoft.Extensions.DependencyInjection;
using Searchfight.Competition;
using Searchfight.Utils;

namespace Searchfight
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var competition = ServiceProvider.Provider.GetService<ICompetition>();

                competition.Run(args, Console.Out).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
