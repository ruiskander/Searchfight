using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight;
using Searchfight.SearchTotalResult;
using Searchfight.Utils;

namespace Tests.Searchfight.Utils
{
    [TestClass]
    public class ServiceProviderTest
    {
        [TestMethod]
        public void TestGetWebSearchEngines()
        {
            var webSearchEngines = ServiceProvider.Provider.GetService<IDictionary<WebSearchEngines, IWebSearchTotalResults>>();

            Assert.IsNotNull(webSearchEngines);

            Assert.AreEqual(2, webSearchEngines.Count);
            Assert.IsNotNull(webSearchEngines[WebSearchEngines.Google]);
            Assert.IsNotNull(webSearchEngines[WebSearchEngines.Bing]);
        }
    }
}
