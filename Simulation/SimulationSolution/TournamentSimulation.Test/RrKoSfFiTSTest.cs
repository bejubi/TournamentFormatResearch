using TournamentSimulation.TournamentStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TournamentSimulation;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for RrKoSfFiTSTest and is intended
    ///to contain all RrKoSfFiTSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RrKoSfFiTSTest
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
        ///A test for BreakTies
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            CompetitorRanks roundRobinRanks = new CompetitorRanks();
            CompetitorRanks finalRanks = new CompetitorRanks();
            int i = 1;
            foreach (Competitor competitor in competitors)
            {
                roundRobinRanks.Add(competitor, i);

                if (i < 5)
                    finalRanks.Add(competitor, 5);
                else if (i < 7)
                    finalRanks.Add(competitor, 3);
                else
                    finalRanks.Add(competitor, 9-i);

                i++;
            }

            RrKoSfFiTS_Accessor target = new RrKoSfFiTS_Accessor();
            target.BreakTiesByRoundRobinRanks(roundRobinRanks, finalRanks);

            var finalRankArray = finalRanks
                .Select(x => new { x.Key.Name, x.Value })
                .OrderBy(x => x.Value)
                .ToArray();

            Assert.AreEqual("H", finalRankArray[0].Name);
            Assert.AreEqual("G", finalRankArray[1].Name);
            Assert.AreEqual("E", finalRankArray[2].Name);
            Assert.AreEqual("F", finalRankArray[3].Name);
            Assert.AreEqual("A", finalRankArray[4].Name);
            Assert.AreEqual("B", finalRankArray[5].Name);
            Assert.AreEqual("C", finalRankArray[6].Name);
            Assert.AreEqual("D", finalRankArray[7].Name);
        }
    }
}
