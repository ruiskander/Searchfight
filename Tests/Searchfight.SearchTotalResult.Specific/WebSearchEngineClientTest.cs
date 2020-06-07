using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searchfight.SearchTotalResult;
using Searchfight.SearchTotalResult.Specific;
using Searchfight.WebSearch;
using Searchfight.WebSearch.Configuration;

namespace Tests.Searchfight.SearchTotalResult.Specific
{
    [TestClass]
    public class WebSearchTotalResultsTest
    {
        [TestMethod]
        public void TestGetTotalResults()
        {
            var query = "test";
            var result = "10";
            var doRequestWasCalled = false;

            Mock<ISearchEngineConfiguration> configuration = new Mock<ISearchEngineConfiguration>();


            Mock<ISearchEngineClient> searchEngine = new Mock<ISearchEngineClient>();
            searchEngine.Setup(c => c.DoRequest(configuration.Object, query))
                .Returns(async () =>
                {
                    await Task.Run(() => { });
                    doRequestWasCalled = true;
                    return result;
                });

            Mock<ISearchResponseParser> parser = new Mock<ISearchResponseParser>();
            parser.Setup(c => c.GetTotalResults(result))
                .Returns(long.Parse(result));

            var webSearchTotalResults = new WebSearchTotalResults(configuration.Object, searchEngine.Object, parser.Object);

            var request = webSearchTotalResults.GetTotalResults(query).Result;

            Assert.AreEqual(long.Parse(result), request);
            Assert.IsTrue(doRequestWasCalled);
        }
    }
}
