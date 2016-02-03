using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using TournamentSimulation;
using TournamentSimulation.MatchStrategies;
using System;
using System.Diagnostics;

namespace TournamentSimulation.Test
{
    /// <summary>
    ///This is a test class for MatchTest and is intended
    ///to contain all MatchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MatchTest
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

        [TestMethod()]
        public void RunTest()
        {
            Competitor competitorA = new Competitor() { Name = "A", TheoreticalRating = 60 };
            Competitor competitorB = new Competitor() { Name = "B", TheoreticalRating = 50 };

            Match match = new Match(new NonRandomMs(), 1);
            match.Run(competitorA, competitorB);

            Assert.AreEqual(competitorA, match.Winner);
            Assert.AreEqual(competitorB, match.Loser);
        }

        [TestMethod()]
        public void RunTest_Rating()
        {
            Competitor competitorA = new Competitor() { Name = "A", TheoreticalRating = 50 };
            Competitor competitorB = new Competitor() { Name = "B", TheoreticalRating = 60 };

            Match match = new Match(new NonRandomMs(), 1);
            match.Run(competitorA, competitorB);

            Assert.AreEqual(competitorB, match.Winner);
            Assert.AreEqual(competitorA, match.Loser);
        }

        [TestMethod()]
        public void RunTest_SimpleRandomMatchStrategy()
        {
            Competitor competitorA = new Competitor() { Name = "A", TheoreticalRating = 60 };
            Competitor competitorB = new Competitor() { Name = "B", TheoreticalRating = 40 };

            Match match = new Match(new SimpleRandomMs(), 1);

            int wins = 0;
            for (int i = 0; i < 100; i++)
            {
                match.Run(competitorA, competitorB);
                if (match.Winner.Name == "A")
                    wins++;
            }
        }

        [TestMethod()]
        public void Test_RandomRatio()
        {
            Random randomNumber = new Random();

            for (int j = 0; j < 100; j++)
            {
                int numberOfWins = 0;
                for (int i = 0; i < 100; i++)
                {
                    double randomRatio = Convert.ToDouble(randomNumber.Next(0, 101)) / Convert.ToDouble(100);
                    double ratingRatio = Convert.ToDouble(60) / Convert.ToDouble(60 + 40);

                    if (randomRatio < ratingRatio)
                    {
                        numberOfWins++;
                    }
                }

                Trace.Write(string.Format("{0}, ", numberOfWins));
            }
        }

        //[TestMethod()]
        //[DeploymentItem("TournamentSimulation.dll")]
        //public void WinnerLoserTest()
        //{
        //    Match_Accessor target = new Match_Accessor();
        //    target.Winner = new Competitor() { Name = "A" };
        //    target.Loser = new Competitor() { Name = "B" };

        //    Assert.AreEqual("A", target.Winner.Name);
        //    Assert.AreEqual("B", target.Loser.Name);
        //}
    }
}
