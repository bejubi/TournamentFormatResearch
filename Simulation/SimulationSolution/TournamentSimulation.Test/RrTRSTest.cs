using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using TournamentSimulation.TournamentRoundStrategies;
using TournamentSimulation;
using TournamentSimulation.MatchStrategies;
using System;

namespace TournamentSimulation.Test
{
    [TestClass()]
    public class RrTRSTest
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
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest_NonRandomMatches()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            var matches = new List<Match>();

            matches.Add((Match)new Match_Accessor() { Winner = competitors[0], Loser = competitors[1], RunSequence = 1 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[0], Loser = competitors[2], RunSequence = 2 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[0], Loser = competitors[4], RunSequence = 3 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[0], Loser = competitors[5], RunSequence = 4 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[1], Loser = competitors[2], RunSequence = 5 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[1], Loser = competitors[3], RunSequence = 6 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[1], Loser = competitors[4], RunSequence = 7 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[1], Loser = competitors[5], RunSequence = 8 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[2], Loser = competitors[0], RunSequence = 9 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[2], Loser = competitors[3], RunSequence = 10 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[2], Loser = competitors[4], RunSequence = 11 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[2], Loser = competitors[5], RunSequence = 12 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[3], Loser = competitors[4], RunSequence = 13 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[4], Loser = competitors[5], RunSequence = 14 }.Target);
            matches.Add((Match)new Match_Accessor() { Winner = competitors[5], Loser = competitors[3], RunSequence = 15 }.Target);

            CompetitorRanks tournamentRoundRanks = new CompetitorRanks();
            tournamentRoundRanks.Add(competitors[0], 1);
            tournamentRoundRanks.Add(competitors[1], 2);
            tournamentRoundRanks.Add(competitors[2], 2);
            tournamentRoundRanks.Add(competitors[3], 4);
            tournamentRoundRanks.Add(competitors[4], 4);
            tournamentRoundRanks.Add(competitors[5], 4);

            while (RrTRS_Accessor.BreakTies(tournamentRoundRanks, matches)) { };

            Assert.AreEqual(1, tournamentRoundRanks[competitors[0]]);
            Assert.AreEqual(2, tournamentRoundRanks[competitors[1]]);
            Assert.AreEqual(3, tournamentRoundRanks[competitors[2]]);
            Assert.AreEqual(4, tournamentRoundRanks[competitors[4]]);
            Assert.AreEqual(5, tournamentRoundRanks[competitors[5]]);
            Assert.AreEqual(6, tournamentRoundRanks[competitors[3]]);
        }

        [TestMethod()]
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            TournamentRound round = new TournamentRound(new RrTRS(), new SimpleRandomMS());
            round.Run(competitors);

            CompetitorRanks tournamentRoundRanks = new CompetitorRanks();
            tournamentRoundRanks.Add(competitors[0], 1);
            tournamentRoundRanks.Add(competitors[1], 2);
            tournamentRoundRanks.Add(competitors[2], 2);
            tournamentRoundRanks.Add(competitors[3], 4);
            tournamentRoundRanks.Add(competitors[4], 4);
            tournamentRoundRanks.Add(competitors[5], 4);

            RrTRS_Accessor.BreakTies(tournamentRoundRanks, round.Matches);

            Assert.IsTrue(tournamentRoundRanks[competitors[0]] < tournamentRoundRanks[competitors[1]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[0]] < tournamentRoundRanks[competitors[2]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[1]] < tournamentRoundRanks[competitors[3]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[1]] < tournamentRoundRanks[competitors[4]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[1]] < tournamentRoundRanks[competitors[5]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[2]] < tournamentRoundRanks[competitors[3]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[2]] < tournamentRoundRanks[competitors[4]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[2]] < tournamentRoundRanks[competitors[5]]);
        }

        [TestMethod()]
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest_ThreeWayTie()
        {
            List<Competitor> competitors = new List<Competitor>();
            competitors.Add(new Competitor() { Name = "A" });
            competitors.Add(new Competitor() { Name = "B" });
            competitors.Add(new Competitor() { Name = "C" });

            List<Match> matches = new List<Match>();

            Match_Accessor match_accessor;

            match_accessor = new Match_Accessor() { Winner = competitors[0], Loser = competitors[1], RunSequence = 1 };
            matches.Add((Match)match_accessor.Target);

            match_accessor = new Match_Accessor() { Winner = competitors[1], Loser = competitors[2], RunSequence = 2 };
            matches.Add((Match)match_accessor.Target);

            match_accessor = new Match_Accessor() { Winner = competitors[2], Loser = competitors[0], RunSequence = 3 };
            matches.Add((Match)match_accessor.Target);

            CompetitorRanks tournamentRoundRanks = new CompetitorRanks();
            tournamentRoundRanks.Add(competitors[0], 1);
            tournamentRoundRanks.Add(competitors[1], 1);
            tournamentRoundRanks.Add(competitors[2], 1);

            while (RrTRS_Accessor.BreakTies(tournamentRoundRanks, matches)) { };

            Assert.IsTrue(tournamentRoundRanks[competitors[1]] < tournamentRoundRanks[competitors[2]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[2]] < tournamentRoundRanks[competitors[0]]);
        }

        /// <summary>
        ///A test for GenerateSingleRoundResult
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TournamentSimulation.dll")]
        public void GenerateSingleRoundResultTest()
        {
            RrTRS_Accessor target = new RrTRS_Accessor();
            MatchStrategy matchStrategy = new NonRandomMS();

            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            double fractionOfPartialRound = 1.0;
            List<Match> actual = target.GenerateSingleRoundResult(matchStrategy, competitors, fractionOfPartialRound);

            Assert.AreEqual(15, actual.Count);
        }

        /// <summary>
        ///A test for GenerateSingleRoundResult
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TournamentSimulation.dll")]
        public void GenerateSingleRoundResultTest_PartialRoundRobin()
        {
            RrTRS_Accessor target = new RrTRS_Accessor();
            MatchStrategy matchStrategy = new NonRandomMS();

            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            double fractionOfPartialRound = 0.8;
            List<Match> actual = target.GenerateSingleRoundResult(matchStrategy, competitors, fractionOfPartialRound);

            Assert.AreEqual(12, actual.Count);
        }

        [TestMethod()]
        public void GenerateResultTest_MultipleRoundsPartialRoundNonRandomMatches()
        {
            RrTRS trs = new RrTRS(2.5);
            MatchStrategy matchStrategy = new NonRandomMS();
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            CompetitorRanks ranks = trs.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(36, trs.Matches.Count);

            Assert.AreEqual(1, ranks[competitors[0]]);
            Assert.AreEqual(2, ranks[competitors[1]]);
            Assert.AreEqual(3, ranks[competitors[2]]);
            Assert.AreEqual(4, ranks[competitors[3]]);
            Assert.AreEqual(5, ranks[competitors[4]]);
            Assert.AreEqual(6, ranks[competitors[5]]);
        }

        [TestMethod()]
        public void GenerateResultTest_MultipleRoundsNonRandomMatches()
        {
            RrTRS trs = new RrTRS(2.0);
            MatchStrategy matchStrategy = new NonRandomMS();
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            CompetitorRanks ranks = trs.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(30, trs.Matches.Count);
        }

        [TestMethod()]
        public void GenerateResultTest_MultipleRoundsPartialRound_2_5()
        {
            RrTRS trs = new RrTRS(2.5);
            MatchStrategy matchStrategy = new SimpleRandomMS();
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            CompetitorRanks ranks = trs.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(36, trs.Matches.Count);
        }
    }
}
