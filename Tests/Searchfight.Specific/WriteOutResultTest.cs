using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Searchfight;
using Searchfight.Competition;
using Searchfight.Specific;

namespace Tests.Searchfight.Specific
{
    [TestClass]
    public class WriteOutResultTest
    {
        private WriteOutResult _writeOutResult;

        [TestInitialize]
        public void Init()
        {
            _writeOutResult = new WriteOutResult();
        }

        [TestMethod]
        public void TestWrite()
        {
            var @out = new StringWriter();

            var competitors = new[] { ".net", "java" };

            Mock<ICompetitionResult> mock = new Mock<ICompetitionResult>();
            mock.Setup(c => c.GetWinner(WebSearchEngines.Google)).Returns(competitors[0]);
            mock.Setup(c => c.GetWinner(WebSearchEngines.Bing)).Returns(competitors[1]);
            mock.Setup(c => c.GetResult(WebSearchEngines.Google, competitors[0])).Returns(14);
            mock.Setup(c => c.GetResult(WebSearchEngines.Google, competitors[1])).Returns(11);
            mock.Setup(c => c.GetResult(WebSearchEngines.Bing, competitors[0])).Returns(10);
            mock.Setup(c => c.GetResult(WebSearchEngines.Bing, competitors[1])).Returns(12);
            mock.Setup(c => c.GetTotalWinner()).Returns(".net");

            _writeOutResult.Write(@out, mock.Object, competitors);

            Assert.AreEqual(@".net: Google: 14 Bing: 10 
java: Google: 11 Bing: 12 
Google winner: .net
Bing winner: java
Total winner: .net
", @out.ToString());
        }
    }
}
