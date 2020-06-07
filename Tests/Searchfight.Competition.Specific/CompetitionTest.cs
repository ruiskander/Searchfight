using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searchfight;
using Searchfight.Competition;
using Searchfight.SearchTotalResult;

namespace Tests.Searchfight.Competition.Specific
{
    [TestClass]
    public class CompetitionTest
    {
        [TestMethod]
        public void AddRun()
        {
            var args = new[] { ".net", "java" };
            var queriesResults = new Dictionary<WebSearchEngines, Dictionary<string, long>>
            {
                {WebSearchEngines.Google, new Dictionary<string, long>(new []{new KeyValuePair<string, long>(args[0], 10), new KeyValuePair<string, long>(args[1], 12)})},
                {WebSearchEngines.Bing, new Dictionary<string, long>(new []{new KeyValuePair<string, long>(args[0], 11), new KeyValuePair<string, long>(args[1], 14) })}
            };

            var webSearchers = queriesResults.Select(p =>
                {
                    Mock<IWebSearchTotalResults> searchTotalResult = new Mock<IWebSearchTotalResults>();
                    foreach (var pair in p.Value)
                    {
                        searchTotalResult.Setup(c => c.GetTotalResults(pair.Key))
                            .Returns(async () =>
                            {
                                await Task.Run(() => { });
                                return pair.Value;
                            });
                    }

                    return new KeyValuePair<WebSearchEngines, IWebSearchTotalResults>(p.Key, searchTotalResult.Object);
                }
            ).ToDictionary(p => p.Key, p => p.Value);

            var counter = 0;

            Mock<ICompetitionResult> competitionResult = new Mock<ICompetitionResult>();
            foreach (var p in queriesResults)
            {
                foreach (var pair in p.Value)
                {
                    competitionResult
                        .Setup(c => c.AddResult(p.Key, pair.Key, pair.Value))
                        .Callback(() => { ++counter; });
                }
            }

            Mock<IWriteOutResult> writeOutResult = new Mock<IWriteOutResult>();
            writeOutResult.Setup(w => w.Write(It.IsAny<TextWriter>(), It.IsAny<ICompetitionResult>(), It.IsAny<IEnumerable<string>>()));

            Mock<ICommandLineParser> parser = new Mock<ICommandLineParser>();
            parser.Setup(p => p.GetCompetitors(args))
                .Returns(() => new List<string>(args));

            var competition = new global::Searchfight.Competition.Specific.Competition(
                webSearchers, competitionResult.Object, writeOutResult.Object, parser.Object);

            var @out = new StringWriter();

            competition.Run(args, @out).Wait();

            Assert.AreEqual(4, counter);
        }
    }
}
