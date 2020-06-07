using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight.Competition;
using Searchfight.Specific;

namespace Tests.Searchfight.Specific
{
    [TestClass]
    public class CommandLineParserTest
    {
        private CommandLineParser _parser;

        [TestInitialize]
        public void Init()
        {
            _parser = new CommandLineParser();
        }

        [TestMethod]
        public void TestWithSimpleWords()
        {
            var args = new[] {"first", "second"};
            var competitors = _parser.GetCompetitors(args);

            PrintArr("Args: ", args);

            Assert.IsNotNull(competitors);
            Assert.AreEqual(2, competitors.Count);

            PrintArr("Competitors: ", competitors);

            for (int i = 0; i < args.Length; i++)
            {
                Assert.IsTrue(competitors[i] == args[i]);
            }
        }

        [TestMethod]
        public void TestCompetitorsLessThanTwoException()
        {
            var args = new[] { "first" };

            PrintArr("Args: ", args);

            Assert.ThrowsException<CompetitorsLessThanTwoException>(() =>
            {
                var competitors = _parser.GetCompetitors(args);
            });
        }

        [TestMethod]
        public void TestWithQuotes()
        {
            var args = new[] { "first", "\"second1", "second2", "second3\"", "third" };
            var competitors = _parser.GetCompetitors(args);

            PrintArr("Args: ", args);

            Assert.IsNotNull(competitors);
            Assert.AreEqual(3, competitors.Count);

            PrintArr("Competitors: ", competitors);

            Assert.IsTrue(competitors[0] == args[0]);
            Assert.IsTrue(competitors[1] == "second1 second2 second3");
            Assert.IsTrue(competitors[2] == args[4]);
        }

        [TestMethod]
        public void TestWithQuotesFirst()
        {
            var args = new[] {"\"first1", "first2\"", "second", "third" };
            var competitors = _parser.GetCompetitors(args);

            PrintArr("Args: ", args);

            Assert.IsNotNull(competitors);
            Assert.AreEqual(3, competitors.Count);

            PrintArr("Competitors: ", competitors);

            Assert.IsTrue(competitors[0] == "first1 first2");
            Assert.IsTrue(competitors[1] == args[2]);
            Assert.IsTrue(competitors[2] == args[3]);
        }

        [TestMethod]
        public void TestWithSeveralQuotes()
        {
            var args = new[] { "\"first1", "first2\"", "second", "\"third1", "third2\"" };
            var competitors = _parser.GetCompetitors(args);

            PrintArr("Args: ", args);

            Assert.IsNotNull(competitors);
            Assert.AreEqual(3, competitors.Count);

            PrintArr("Competitors: ", competitors);

            Assert.IsTrue(competitors[0] == "first1 first2");
            Assert.IsTrue(competitors[1] == args[2]);
            Assert.IsTrue(competitors[2] == "third1 third2");
        }

        [TestMethod]
        public void TestWithDuplicates()
        {
            var args = new[] { "\"first1", "first2\"", "second", "\"first1", "first2\"" };
            var competitors = _parser.GetCompetitors(args);

            PrintArr("Args: ", args);

            Assert.IsNotNull(competitors);
            Assert.AreEqual(2, competitors.Count);

            PrintArr("Competitors: ", competitors);

            Assert.IsTrue(competitors[0] == "first1 first2");
            Assert.IsTrue(competitors[1] == args[2]);
        }

        private void PrintArr(string message, IEnumerable<string> arr)
        {
            Console.WriteLine(message + string.Join(", ", arr.Select(s => $"[{s}]")));
        }
    }
}
