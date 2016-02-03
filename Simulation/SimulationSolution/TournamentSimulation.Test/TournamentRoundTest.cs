using TournamentSimulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentSimulation.TournamentRoundStrategies;
using TournamentSimulation.MatchStrategies;
using System.Collections.Generic;
using System.Diagnostics;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for TournamentRoundTest and is intended
    ///to contain all TournamentRoundTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TournamentRoundTest
    {
        #region Setup

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        #endregion

        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest()
        {
            List<Competitor> competitors = new List<Competitor>();
            competitors.Add(new Competitor() { Name = "A", TheoreticalRating = 90 });
            competitors.Add(new Competitor() { Name = "B", TheoreticalRating = 80 });
            competitors.Add(new Competitor() { Name = "C", TheoreticalRating = 70 });
            competitors.Add(new Competitor() { Name = "D", TheoreticalRating = 60 });

            int initialRank = 1;
            foreach (Competitor competitor in competitors)
            {
                competitor.InitialRank = initialRank++;
            }

            TournamentRound round = new TournamentRound(new KoTRS(1), new NonRandomMs());
            round.Run(competitors);

            foreach (Match match in round.Matches)
            {
                Trace.WriteLine(string.Format("Match {0}: Winner {1}, Loser {2}", match.RunSequence, match.Winner.Name, match.Loser.Name));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void RunTest_OddCompetitors()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(7);

            TournamentRound round = new TournamentRound(new KoTRS(1), new NonRandomMs());
            round.Run(competitors);
        }

        [TestMethod()]
        public void RunTest_RoundRobin()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(7);

            TournamentRound round = new TournamentRound(new RrTRS(), new NonRandomMs());
            round.Run(competitors);

            foreach (Match match in round.Matches)
            {
                Trace.WriteLine(string.Format("Match {0}: Winner {1}, Loser {2}", match.RunSequence, match.Winner.Name, match.Loser.Name));
            }
        }
    }
}
