using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight.Configuration.Specific.Json;

namespace Tests.Searchfight.Configuration.Specific
{
    [TestClass]
    public class SearchEnginesConfigurationTest
    {
        [TestMethod]
        public void TestGetSearchEngineSettings()
        {
            var searchEnginesConfiguration = new SearchEnginesConfiguration();

            var searchEnginesSettings = searchEnginesConfiguration.GetSearchEngineSettings();

            Assert.IsNotNull(searchEnginesSettings);

            var searchEnginesSettingsList = searchEnginesSettings.ToList();
            Assert.AreEqual(2, searchEnginesSettingsList.Count);

            var google = searchEnginesSettingsList.FirstOrDefault(s => s.Name == "Google");
            Assert.IsNotNull(google);
            Assert.IsTrue(!string.IsNullOrEmpty(google.BaseUri));
            Assert.IsNotNull(google.Parameters);
            Assert.AreEqual(2, google.Parameters.Count());

            var bing = searchEnginesSettingsList.FirstOrDefault(s => s.Name == "Bing");
            Assert.IsNotNull(bing);
            Assert.IsTrue(!string.IsNullOrEmpty(bing.BaseUri));
            Assert.IsNotNull(bing.Headers);
            Assert.AreEqual(1, bing.Headers.Count());
        }
    }
}
