using TournamentSimulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for CompetitorPointsTest and is intended
    ///to contain all CompetitorPointsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompetitorPointsTest
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
        ///A test for GetCompetitorRanks
        ///</summary>
        [TestMethod()]
        public void GetCompetitorRanksTest()
        {
            var points = new CompetitorPoints();

            double pointValue = 1.0;
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);
            foreach (var competitor in competitors)
            {
                points.Add(competitor, pointValue++);
            }
            
            var ranks = points.GetCompetitorRanks();

            Assert.AreEqual(1, ranks[competitors[7]]);
            Assert.AreEqual(2, ranks[competitors[6]]);
            Assert.AreEqual(competitors.Count, ranks[competitors[0]]);
        }
    }
}
