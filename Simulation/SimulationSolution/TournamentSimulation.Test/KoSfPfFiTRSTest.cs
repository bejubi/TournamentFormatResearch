using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TournamentSimulation;
using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentRoundStrategies;
using System;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for KnockoutSemisFinalsPetitsTRSTest and is intended
    ///to contain all KnockoutSemisFinalsPetitsTRSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KoSfPfFiTRSTest
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
            int winsToClinchSemifinalMatch = 2;
            int winsToClinchFinalsMatch = 3;
            int winsToClinchPetitMatch = 2;
            KoSfPfFiTRS_Accessor knockoutSemisFinalsPetitsTRS = new KoSfPfFiTRS_Accessor(winsToClinchSemifinalMatch, winsToClinchFinalsMatch, winsToClinchPetitMatch);

            MatchStrategy matchStrategy = new NonRandomMS();
            List<Competitor> competitors = new List<Competitor>();
            competitors.Add(new Competitor() { Name = "A", TheoreticalRating = 90 });
            competitors.Add(new Competitor() { Name = "B", TheoreticalRating = 80 });
            competitors.Add(new Competitor() { Name = "C", TheoreticalRating = 70 });
            competitors.Add(new Competitor() { Name = "D", TheoreticalRating = 60 });

            CompetitorRanks ranks = knockoutSemisFinalsPetitsTRS.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(1, ranks[competitors[0]]);
            Assert.AreEqual(2, ranks[competitors[1]]);
            Assert.AreEqual(3, ranks[competitors[2]]);
            Assert.AreEqual(4, ranks[competitors[3]]);
        }
    }
}
