using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight.SearchTotalResult.Specific.Bing;

namespace Tests.Searchfight.SearchTotalResult.Specific
{
    [TestClass]
    public class BingSearchResponseParserTest
    {
        private BingSearchResponseParser _parser;
        
        [TestInitialize]
        public void Init()
        {
            _parser = new BingSearchResponseParser();
        }

        [TestMethod]
        public void TestGetTotalResults()
        {
            var response = Helper.GetResourceFileText("Tests.Resource.BingValidResponse.json");

            long totalResults = _parser.GetTotalResults(response);

            Assert.AreEqual(64200000, totalResults);
        }
    }
}
