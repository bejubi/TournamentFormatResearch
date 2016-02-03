using TournamentSimulation.TournamentRoundStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for KnockoutSemisFinalsPetits5TRSTest and is intended
    ///to contain all KnockoutSemisFinalsPetits5TRSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KoSf5PfFiTRSTest
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

        /// <summary>
        ///A test for GenerateResult
        ///</summary>
        [TestMethod]
        public void GenerateResultTest()
        {
           
        }

        /// <summary>
        ///A test for GetTournamentRoundRanks
        ///</summary>
        [TestMethod]
        public void GetTournamentRoundRanksTest()
        {
            var target = new KoSf5PfFiTRS(2, 2, 2);
            MatchStrategy matchStrategy = new NonRandomMs();
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(5);
            var competitorRanks = target.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(1, competitorRanks[competitors[0]]);
            Assert.AreEqual(2, competitorRanks[competitors[1]]);
            Assert.AreEqual(3, competitorRanks[competitors[2]]);
            Assert.AreEqual(4, competitorRanks[competitors[3]]);
            Assert.AreEqual(5, competitorRanks[competitors[4]]);
        }
    }
}
