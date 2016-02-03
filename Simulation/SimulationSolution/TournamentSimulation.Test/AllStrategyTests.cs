using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentStrategies;

namespace TournamentSimulation.Test
{
    /// <summary>
    /// This is the set of tests to generate the results we're interested in studying.
    /// </summary>
    /// <remarks>
    /// All functional tests are in other test classes.
    /// </remarks>
    [TestClass()]
    public class AllStrategyTests
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

        private const int _numberOfTournamentIterations = 10;

        [TestMethod()]
        public void RunTest_KoTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            var study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_KoTS_BadSeeding()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(8);

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrTS_1()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(8);

            TournamentStrategy tournamentStrategy = new RrTS(1, 1);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations, true);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrTS_2()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(5);

            TournamentStrategy tournamentStrategy = new RrTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations, true);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrTS_2_5()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(5);

            TournamentStrategy tournamentStrategy = new RrTS(2.5);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations, true);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrTS_3()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(5);

            TournamentStrategy tournamentStrategy = new RrTS(3);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations, true);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrTS_10()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(5);

            TournamentStrategy tournamentStrategy = new RrTS(10);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations, true);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrTS_1_FirstTo2()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(8);

            TournamentStrategy tournamentStrategy = new RrTS(1, 2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations, true);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrKoSfFiTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrKoSfFiTS(2.0, 2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrKoSf5PfFiTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(5);

            TournamentStrategy tournamentStrategy = new RrKoSf5PfFiTS(2.0, 2, 2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrKoSfPfFiTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrKoSfPfFiTS(2.0, 2, 2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrKoSfPfFiRrTS_ReferenceFormat()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrKoSfPfFiRrTS(2.0, 2, 2, 2, 2.0);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }
        
        [TestMethod()]
        public void RunTest_RrRrSf5KoFiPfTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrRrSf5KoFiPfTS(2.0, 1.0, 2, 2, 1.0);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrKo1TS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrKo1TS(2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        [TestMethod()]
        public void RunTest_RrKo1aTS()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            TournamentStrategy tournamentStrategy = new RrKo1aTS(2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMs();

            Study study = new Study(tournamentStrategy, matchStrategy, false);
            study.Run(competitors, _numberOfTournamentIterations);

            DisplayTestResults(tournamentStrategy, matchStrategy, study);
        }

        #region Helper Methods
        private void DisplayTestResults(TournamentStrategy tournamentStrategy, MatchStrategy matchStrategy, Study study)
        {
            Trace.WriteLine(string.Format("Number of Tournament Iterations: {0}", _numberOfTournamentIterations));
            Trace.WriteLine(study.StrategyInformation);
        }
        #endregion
    }
}
