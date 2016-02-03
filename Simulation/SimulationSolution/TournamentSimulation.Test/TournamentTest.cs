using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using TournamentSimulation;
using TournamentSimulation.TournamentRoundStrategies;
using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentStrategies;
using System.Diagnostics;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for TournamentTest and is intended
    ///to contain all TournamentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TournamentTest
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
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            Tournament tournament = new Tournament(new KoTS(), new NonRandomMs());
            tournament.Run(competitors);
        }

        [TestMethod()]
        public void RunTest_KoTS()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            Tournament tournament = new Tournament(new KoTS(), new NonRandomMs());
            tournament.Run(competitors);
        }

        [TestMethod()]
        public void RunTest_RrTS()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            Tournament tournament = new Tournament(new RrTS(), new SimpleRandomMs());
            tournament.Run(competitors);
        }
    }
}
