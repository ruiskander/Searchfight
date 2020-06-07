using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searchfight.WebSearch;
using Searchfight.WebSearch.Configuration;

namespace Tests.Searchfight.WebSearch
{
    [TestClass]
    public class WebSearchEngineClientTest
    {
        private WebSearchEngineClient _engineClient;

        [TestInitialize]
        public void Init()
        {
            _engineClient = new WebSearchEngineClient();
        }

        [TestMethod]
        public void TestDoRequest()
        {
            var query = "test";
            Mock<ISearchEngineConfiguration> mock = new Mock<ISearchEngineConfiguration>();
            mock.Setup(c => c.GetRequestUri(query))
                .Returns("https://www.google.com");

            mock.Setup(c => c.GetRequestHeaders())
                .Returns((IDictionary<string, string>) null);

            var request = _engineClient.DoRequest(mock.Object, query).Result;

            Assert.IsNotNull(request);
        }

        [TestMethod]
        public void TestDoRequestWithHeaders()
        {
            var query = "test";
            Mock<ISearchEngineConfiguration> mock = new Mock<ISearchEngineConfiguration>();
            mock.Setup(c => c.GetRequestUri(query))
                .Returns("https://www.google.com");

            mock.Setup(c => c.GetRequestHeaders())
                .Returns(() => new Dictionary<string, string>(new []{new KeyValuePair<string, string>("key", "value") }));

            var request = _engineClient.DoRequest(mock.Object, query).Result;

            Assert.IsNotNull(request);
        }
    }
}
