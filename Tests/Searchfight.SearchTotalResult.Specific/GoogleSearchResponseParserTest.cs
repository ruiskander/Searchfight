using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight.SearchTotalResult.Specific.Google;

namespace Tests.Searchfight.SearchTotalResult.Specific
{
    [TestClass]
    public class GoogleSearchResponseParserTest
    {
        private GoogleSearchResponseParser _parser;
        
        [TestInitialize]
        public void Init()
        {
            _parser = new GoogleSearchResponseParser();
        }

        [TestMethod]
        public void TestGetTotalResults()
        {
            var response = Helper.GetResourceFileText("Tests.Resource.GoogleValidResponse.json");

            long totalResults = _parser.GetTotalResults(response);

            Assert.AreEqual(793000000, totalResults);
        }
    }
}
