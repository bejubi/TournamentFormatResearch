using TournamentSimulation.TournamentRoundStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentSimulation.MatchStrategies;
using TournamentSimulation;
using System.Collections.Generic;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for KnockoutWithTiesTRSTest and is intended
    ///to contain all KnockoutWithTiesTRSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KoSfFiTRSTest
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
        ///A test for GetTournamentRoundRanks
        ///</summary>
        [TestMethod()]
        public void GetTournamentRoundRanksTest()
        {
            KoSfFiTRS target = new KoSfFiTRS(2);

            MatchStrategy matchStrategy = new NonRandomMS();
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            CompetitorRanks ranks = target.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(1, ranks[competitors[0]]);
            Assert.AreEqual(2, ranks[competitors[1]]);
            Assert.AreEqual(3, ranks[competitors[2]]);
            Assert.AreEqual(3, ranks[competitors[3]]);
            Assert.AreEqual(5, ranks[competitors[4]]);
            Assert.AreEqual(5, ranks[competitors[5]]);
            Assert.AreEqual(5, ranks[competitors[6]]);
            Assert.AreEqual(5, ranks[competitors[7]]);
        }
    }
}