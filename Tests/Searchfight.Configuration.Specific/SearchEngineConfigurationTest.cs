using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searchfight;
using Searchfight.Configuration;
using Searchfight.Configuration.Specific;

namespace Tests.Searchfight.Configuration.Specific
{
    [TestClass]
    public class SearchEngineConfigurationTest
    {
        [TestMethod]
        public void TestGetRequestUri()
        {
            var baseUri = "https://www.google.com";
            Mock<ISearchEnginesConfiguration> searchEnginesSettings = new Mock<ISearchEnginesConfiguration>();
            searchEnginesSettings.Setup(c => c.GetSearchEngineSettings())
                .Returns(new List<SearchEngineSettings>()
                {
                    new SearchEngineSettings()
                    {
                        Name = Enum.GetName(typeof(WebSearchEngines), WebSearchEngines.Google),
                        BaseUri = baseUri,
                        Headers = null,
                        Parameters = null
                    }
                });

            var searchEngineConfiguration = new SearchEngineConfiguration(WebSearchEngines.Google, searchEnginesSettings.Object);

            var query = "test";

            var uri = searchEngineConfiguration.GetRequestUri(query);

            Assert.AreEqual($"{baseUri}?q={query}", uri);
        }

        [TestMethod]
        public void TestGetRequestUriWithParameters()
        {
            var baseUri = "https://www.google.com";
            Mock<ISearchEnginesConfiguration> searchEnginesSettings = new Mock<ISearchEnginesConfiguration>();
            searchEnginesSettings.Setup(c => c.GetSearchEngineSettings())
                .Returns(new List<SearchEngineSettings>()
                {
                    new SearchEngineSettings()
                    {
                        Name = Enum.GetName(typeof(WebSearchEngines), WebSearchEngines.Google),
                        BaseUri = baseUri,
                        Headers = null,
                        Parameters = new []
                        {
                            new Parameter(){Name = "key1", Value = "value1"},
                            new Parameter(){Name = "key2", Value = "value2"}
                        }
                    }
                });

            var searchEngineConfiguration = new SearchEngineConfiguration(WebSearchEngines.Google, searchEnginesSettings.Object);

            var query = "test";

            var uri = searchEngineConfiguration.GetRequestUri(query);

            Assert.AreEqual($"{baseUri}?q={query}&key1=value1&key2=value2", uri);
        }

        [TestMethod]
        public void TestGetRequestHeaders()
        {
            var headers = new[]
            {
                new Header() {Key = "key1", Value = "value1"},
                new Header() {Key = "key2", Value = "value2"}
            };
            Mock<ISearchEnginesConfiguration> searchEnginesSettings = new Mock<ISearchEnginesConfiguration>();
            searchEnginesSettings.Setup(c => c.GetSearchEngineSettings())
                .Returns(new List<SearchEngineSettings>()
                {
                    new SearchEngineSettings()
                    {
                        Name = Enum.GetName(typeof(WebSearchEngines), WebSearchEngines.Google),
                        BaseUri = "",
                        Headers = headers,
                        Parameters = null
                    }
                });

            var searchEngineConfiguration = new SearchEngineConfiguration(WebSearchEngines.Google, searchEnginesSettings.Object);

            var headersDic = searchEngineConfiguration.GetRequestHeaders();

            Assert.IsNotNull(headersDic);
            Assert.AreEqual(headers.Length, headersDic.Count);

            for (int i = 0; i < headers.Length; i++)
            { 
                Assert.AreEqual(headers[i].Key, headersDic.Keys.ToList()[i]);
                Assert.AreEqual(headers[i].Value, headersDic.Values.ToList()[i]);
            }
        }

        [TestMethod]
        public void TestGetRequestHeadersDuplicate()
        {
            var headers = new[]
            {
                new Header() {Key = "key1", Value = "value1"},
                new Header() {Key = "key1", Value = "value1"}
            };
            Mock<ISearchEnginesConfiguration> searchEnginesSettings = new Mock<ISearchEnginesConfiguration>();
            searchEnginesSettings.Setup(c => c.GetSearchEngineSettings())
                .Returns(new List<SearchEngineSettings>()
                {
                    new SearchEngineSettings()
                    {
                        Name = Enum.GetName(typeof(WebSearchEngines), WebSearchEngines.Google),
                        BaseUri = "",
                        Headers = headers,
                        Parameters = null
                    }
                });

            var searchEngineConfiguration = new SearchEngineConfiguration(WebSearchEngines.Google, searchEnginesSettings.Object);

            var headersDic = searchEngineConfiguration.GetRequestHeaders();

            Assert.IsNotNull(headersDic);
            Assert.AreEqual(1, headersDic.Count);
            Assert.AreEqual(headers[0].Key, headersDic.Keys.ToList()[0]);
            Assert.AreEqual(headers[0].Value, headersDic.Values.ToList()[0]);
        }

        [TestMethod]
        public void TestGetRequestHeadersEmpty()
        {
            Mock<ISearchEnginesConfiguration> searchEnginesSettings = new Mock<ISearchEnginesConfiguration>();
            searchEnginesSettings.Setup(c => c.GetSearchEngineSettings())
                .Returns(new List<SearchEngineSettings>()
                {
                    new SearchEngineSettings()
                    {
                        Name = Enum.GetName(typeof(WebSearchEngines), WebSearchEngines.Google),
                        BaseUri = "",
                        Headers = null,
                        Parameters = null
                    }
                });

            var searchEngineConfiguration = new SearchEngineConfiguration(WebSearchEngines.Google, searchEnginesSettings.Object);

            var headersDic = searchEngineConfiguration.GetRequestHeaders();

            Assert.IsNotNull(headersDic);
            Assert.AreEqual(0, headersDic.Count);
        }
    }
}
