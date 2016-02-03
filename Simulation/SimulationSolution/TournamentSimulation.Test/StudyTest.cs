using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentStrategies;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for StudyTest and is intended
    ///to contain all StudyTest Unit Tests
    ///</summary>
    [TestClass]
    public class StudyTest
    {
        #region Setup

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        private const int _numberOfTournamentIterations = 10;

        [TestMethod]
        public void RunTest_RoundRobinThenQuarterfinals_NonRandomMatches()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrKoSfFiTS(2.0, 2);
            MatchStrategy matchStrategy = new NonRandomMs();

            var study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);

            Assert.AreEqual(1, competitors[0].TournamentRankMean);
            Assert.AreEqual(2, competitors[1].TournamentRankMean);
            Assert.AreEqual(3, competitors[2].TournamentRankMean);
            Assert.AreEqual(4, competitors[3].TournamentRankMean);
            Assert.AreEqual(5, competitors[4].TournamentRankMean);
            Assert.AreEqual(6, competitors[5].TournamentRankMean);
            Assert.AreEqual(7, competitors[6].TournamentRankMean);
            Assert.AreEqual(8, competitors[7].TournamentRankMean);
        }

        [TestMethod]
        public void RunTest_Knockout()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            var study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            foreach (var competitor in competitors)
            {
                foreach (var rankFrequency in competitor.RankFrequencies.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    Assert.IsTrue(rankFrequency.Key > 0);
                }
            }
        }

        [TestMethod]
        public void RunTest_RrRrSf5KoFiPfTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrRrSf5KoFiPfTS(1.0, 1.0, 2, 2, 1.0);
            MatchStrategy matchStrategy = new NonRandomMs();

            var study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod]
        public void RunTest_Knockout_DominantCompetitors()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithTwoDominants_16();

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            var study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        private static void DisplayTestResults(TournamentStrategy tournamentStrategy, MatchStrategy matchStrategy, Study study)
        {
            Trace.WriteLine("Study Parameters:");
            Trace.WriteLine(string.Format("Tournament Strategy: {0}", tournamentStrategy.GetType()));
            Trace.WriteLine(string.Format("Match Strategy: {0}", matchStrategy.GetType()));
            Trace.WriteLine(string.Format("Number of Tournament Iterations: {0}", _numberOfTournamentIterations));

            Trace.WriteLine("");
            Trace.WriteLine(study.StrategyInformation);
        }
    }
}
