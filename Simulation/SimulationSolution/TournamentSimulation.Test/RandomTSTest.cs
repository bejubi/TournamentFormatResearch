using TournamentSimulation.TournamentStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TournamentSimulation.MatchStrategies;
using TournamentSimulation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for RandomTSTest and is intended
    ///to contain all RandomTSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RandomTSTest
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
            RandomTS target = new RandomTS();
            MatchStrategy matchStrategy = null;
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);
            CompetitorRanks ranks;

            for (int i = 0; i < 20; i++)
            {
                ranks = target.GenerateResult(i, matchStrategy, competitors);

                foreach (KeyValuePair<Competitor, int> rank in ranks.OrderBy(x => x.Value))
                {
                    Trace.Write(string.Format("{0}, ", rank.Key.Name));
                }

                Trace.WriteLine("");
            }
        }
    }
}
