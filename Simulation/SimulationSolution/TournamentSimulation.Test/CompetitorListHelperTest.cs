using TournamentSimulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TournamentSimulation.Helpers;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for CompetitorListHelperTest and is intended
    ///to contain all CompetitorListHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompetitorListHelperTest
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
        ///A test for GetRandomCompetitors
        ///</summary>
        [TestMethod()]
        public void GetRandomCompetitorsTest()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomRatings(8);

            Assert.AreEqual("A", competitors[0].Name);
            Assert.AreEqual("B", competitors[1].Name);
            Assert.AreEqual("C", competitors[2].Name);
            Assert.AreEqual("D", competitors[3].Name);
            Assert.AreEqual("E", competitors[4].Name);
            Assert.AreEqual("F", competitors[5].Name);
            Assert.AreEqual("G", competitors[6].Name);
            Assert.AreEqual("H", competitors[7].Name);

            Assert.IsTrue(competitors[0].TheoreticalRating > competitors[1].TheoreticalRating);

            foreach (Competitor competitor in competitors)
            {
                Trace.WriteLine(string.Format("Competitor {0}: Rating {1}", competitor.Name, competitor.TheoreticalRating));
            }
        }

        /// <summary>
        ///A test for GetStandardCompetitors
        ///</summary>
        [TestMethod()]
        public void GetStandardCompetitorsTest()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(8);

            Assert.AreEqual("A", competitors[0].Name);
            Assert.AreEqual("B", competitors[1].Name);
            Assert.AreEqual("C", competitors[2].Name);
            Assert.AreEqual("D", competitors[3].Name);
            Assert.AreEqual("E", competitors[4].Name);
            Assert.AreEqual("F", competitors[5].Name);
            Assert.AreEqual("G", competitors[6].Name);
            Assert.AreEqual("H", competitors[7].Name);

            Assert.AreEqual(80, competitors[0].TheoreticalRating);
            Assert.AreEqual(70, competitors[1].TheoreticalRating);
            Assert.AreEqual(60, competitors[2].TheoreticalRating);
            Assert.AreEqual(50, competitors[3].TheoreticalRating);
            Assert.AreEqual(40, competitors[4].TheoreticalRating);
            Assert.AreEqual(30, competitors[5].TheoreticalRating);
            Assert.AreEqual(20, competitors[6].TheoreticalRating);
            Assert.AreEqual(10, competitors[7].TheoreticalRating);

            foreach (Competitor competitor in competitors)
            {
                Trace.WriteLine(string.Format("Competitor {0}: Rating {1}", competitor.Name, competitor.TheoreticalRating));
            }
        }

        /// <summary>
        ///A test for GetEvenlySpacedCompetitors
        ///</summary>
        [TestMethod()]
        public void GetEvenlySpacedCompetitorsTest()
        {
            int numberOfCompetitors = 10;
            var competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(numberOfCompetitors);

            Assert.AreEqual("A", competitors[0].Name);
            Assert.AreEqual("J", competitors[9].Name);
            Assert.AreEqual(100, competitors[0].TheoreticalRating);
            Assert.AreEqual(8, competitors[9].TheoreticalRating);

            foreach (Competitor competitor in competitors)
            {
                Trace.WriteLine(string.Format("Competitor {0}: Rating {1}", competitor.Name, competitor.TheoreticalRating));
            }
        }

        /// <summary>
        ///A test for GetCompetitors
        ///</summary>
        [TestMethod()]
        public void GetCompetitorsTest()
        {
            int[] ratings = new int[] { 100, 75, 50, 25 };
            var competitors = CompetitorListHelper.GetCompetitors(ratings);

            Assert.AreEqual("A", competitors[0].Name);
            Assert.AreEqual("D", competitors[3].Name);

            Assert.AreEqual(100, competitors[0].TheoreticalRating);
            Assert.AreEqual(50, competitors[2].TheoreticalRating);

            foreach (Competitor competitor in competitors)
            {
                Trace.WriteLine(string.Format("Competitor {0}: Rating {1}", competitor.Name, competitor.TheoreticalRating));
            }
        }
    }
}
