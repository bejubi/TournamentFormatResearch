using TournamentSimulation.TournamentStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TournamentSimulation.MatchStrategies;
using TournamentSimulation;
using System.Collections.Generic;
using System.Linq;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for RrKo1TSTest and is intended
    ///to contain all RrKo1TSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RrKo1TSTest
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
        ///A test for GenerateResult
        ///</summary>
        [TestMethod()]
        public void GenerateResultTest()
        {
            RrKo1TS target = new RrKo1TS(2, 2);
            int tournamentRunSequence = 1;
            MatchStrategy matchStrategy = new NonRandomMs();
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);
            CompetitorRanks ranks = target.GenerateResult(tournamentRunSequence, matchStrategy, competitors);

            int i = 1;
            foreach (KeyValuePair<Competitor, int> rank in ranks.OrderBy(x => x.Key.Name))
            {
                Assert.AreEqual(i++, rank.Value);
            }
        }
    }
}
