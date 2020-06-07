using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight;
using Searchfight.Competition.Specific;

namespace Tests.Searchfight.Competition.Specific
{
    [TestClass]
    public class CompetitionResultTest
    {
        private CompetitionResult _competitionResult;

        [TestInitialize]
        public void Init()
        {
            _competitionResult = new CompetitionResult();
        }

        [TestMethod]
        public void AddGetResultTest()
        {
            var engine = WebSearchEngines.Google;
            var competitor = "java";
            var result = 10;

            _competitionResult.AddResult(engine, competitor, result);

            Assert.AreEqual(result, _competitionResult.GetResult(engine, competitor));
        }

        [TestMethod]
        public void AddGetResultDuplicateTest()
        {
            var engine = WebSearchEngines.Google;
            var competitor = "java";
            var result = 10;

            _competitionResult.AddResult(engine, competitor, result);
            result = 11;
            _competitionResult.AddResult(engine, competitor, result);

            Assert.AreEqual(result, _competitionResult.GetResult(engine, competitor));
        }

        [TestMethod]
        public void GetResultNullTest()
        {
            var engine = WebSearchEngines.Google;
            var competitor = "java";

            Assert.AreEqual(null, _competitionResult.GetResult(engine, competitor));
        }
    }
}
