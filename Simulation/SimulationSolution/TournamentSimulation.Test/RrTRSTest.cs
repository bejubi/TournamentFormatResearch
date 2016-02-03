using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TournamentSimulation.TournamentRoundStrategies;
using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation.Test
{
    [TestClass]
    public class RrTRSTest
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

        [TestMethod]
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest_NonRandomMatches()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            var matches = new List<Match>
                              {
                                  (Match)new Match_Accessor {
                                          Winner = competitors[0],
                                          Loser = competitors[1],
                                          RunSequence = 1
                                      }.Target,
                                  (Match)new Match_Accessor {
                                          Winner = competitors[0],
                                          Loser = competitors[2],
                                          RunSequence = 2
                                      }.Target,
                                  (Match)new Match_Accessor {
                                          Winner = competitors[0],
                                          Loser = competitors[4],
                                          RunSequence = 3
                                      }.Target,
                                  (Match)new Match_Accessor {
                                          Winner = competitors[0],
                                          Loser = competitors[5],
                                          RunSequence = 4
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[1],
                                          Loser = competitors[2],
                                          RunSequence = 5
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[1],
                                          Loser = competitors[3],
                                          RunSequence = 6
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[1],
                                          Loser = competitors[4],
                                          RunSequence = 7
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[1],
                                          Loser = competitors[5],
                                          RunSequence = 8
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[2],
                                          Loser = competitors[0],
                                          RunSequence = 9
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[2],
                                          Loser = competitors[3],
                                          RunSequence = 10
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[2],
                                          Loser = competitors[4],
                                          RunSequence = 11
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[2],
                                          Loser = competitors[5],
                                          RunSequence = 12
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[3],
                                          Loser = competitors[4],
                                          RunSequence = 13
                                      }.Target,
                                  (Match)
                                  new Match_Accessor
                                      {
                                          Winner = competitors[4],
                                          Loser = competitors[5],
                                          RunSequence = 14
                                      }.Target,
                                  (Match)new Match_Accessor
                                      {
                                          Winner = competitors[5],
                                          Loser = competitors[3],
                                          RunSequence = 15
                                      }.Target
                              };

            var tournamentRoundRanks = new CompetitorRanks
                                           {
                                               { competitors[0], 1 },
                                               { competitors[1], 2 },
                                               { competitors[2], 2 },
                                               { competitors[3], 4 },
                                               { competitors[4], 4 },
                                               { competitors[5], 4 }
                                           };

            while (RrTRS_Accessor.BreakTies(tournamentRoundRanks, matches)) { };

            Assert.AreEqual(1, tournamentRoundRanks[competitors[0]]);
            Assert.AreEqual(2, tournamentRoundRanks[competitors[1]]);
            Assert.AreEqual(3, tournamentRoundRanks[competitors[2]]);
            Assert.AreEqual(4, tournamentRoundRanks[competitors[4]]);
            Assert.AreEqual(5, tournamentRoundRanks[competitors[5]]);
            Assert.AreEqual(6, tournamentRoundRanks[competitors[3]]);
        }

        [TestMethod]
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest()
        {
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            var round = new TournamentRound(new RrTRS(), new SimpleRandomMs());
            round.Run(competitors);

            var tournamentRoundRanks = new CompetitorRanks
                                           {
                                               { competitors[0], 1 },
                                               { competitors[1], 2 },
                                               { competitors[2], 2 },
                                               { competitors[3], 4 },
                                               { competitors[4], 4 },
                                               { competitors[5], 4 }
                                           };

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

        [TestMethod]
        [DeploymentItem("TournamentSimulation.dll")]
        public void BreakTiesTest_ThreeWayTie()
        {
            var competitors = new List<Competitor>
                                  {
                                      new Competitor { Name = "A" },
                                      new Competitor { Name = "B" },
                                      new Competitor { Name = "C" }
                                  };

            var matches = new List<Match>();

            var matchAccessor = new Match_Accessor { Winner = competitors[0], Loser = competitors[1], RunSequence = 1 };
            matches.Add((Match)matchAccessor.Target);

            matchAccessor = new Match_Accessor { Winner = competitors[1], Loser = competitors[2], RunSequence = 2 };
            matches.Add((Match)matchAccessor.Target);

            matchAccessor = new Match_Accessor { Winner = competitors[2], Loser = competitors[0], RunSequence = 3 };
            matches.Add((Match)matchAccessor.Target);

            var tournamentRoundRanks = new CompetitorRanks
                                                       {
                                                           { competitors[0], 1 },
                                                           { competitors[1], 1 },
                                                           { competitors[2], 1 }
                                                       };

            while (RrTRS_Accessor.BreakTies(tournamentRoundRanks, matches)) { };

            Assert.IsTrue(tournamentRoundRanks[competitors[1]] < tournamentRoundRanks[competitors[2]]);
            Assert.IsTrue(tournamentRoundRanks[competitors[2]] < tournamentRoundRanks[competitors[0]]);
        }

        /// <summary>
        ///A test for GenerateSingleRoundResult
        ///</summary>
        [TestMethod]
        [DeploymentItem("TournamentSimulation.dll")]
        public void GenerateSingleRoundResultTest()
        {
            var target = new RrTRS_Accessor();
            MatchStrategy matchStrategy = new NonRandomMs();

            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            const double FractionOfPartialRound = 1.0;
            List<Match> actual = target.GenerateSingleRoundResult(matchStrategy, competitors, FractionOfPartialRound);

            Assert.AreEqual(15, actual.Count);
        }

        /// <summary>
        ///A test for GenerateSingleRoundResult
        ///</summary>
        [TestMethod]
        [DeploymentItem("TournamentSimulation.dll")]
        public void GenerateSingleRoundResultTest_PartialRoundRobin()
        {
            var target = new RrTRS_Accessor();
            MatchStrategy matchStrategy = new NonRandomMs();

            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            const double FractionOfPartialRound = 0.8;
            List<Match> actual = target.GenerateSingleRoundResult(matchStrategy, competitors, FractionOfPartialRound);

            Assert.AreEqual(12, actual.Count);
        }

        [TestMethod]
        public void GenerateResultTest_MultipleRoundsPartialRoundNonRandomMatches()
        {
            var trs = new RrTRS(2.5);
            MatchStrategy matchStrategy = new NonRandomMs();
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            var ranks = trs.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(36, trs.Matches.Count);

            Assert.AreEqual(1, ranks[competitors[0]]);
            Assert.AreEqual(2, ranks[competitors[1]]);
            Assert.AreEqual(3, ranks[competitors[2]]);
            Assert.AreEqual(4, ranks[competitors[3]]);
            Assert.AreEqual(5, ranks[competitors[4]]);
            Assert.AreEqual(6, ranks[competitors[5]]);
        }

        [TestMethod]
        public void GenerateResultTest_MultipleRoundsNonRandomMatches()
        {
            var trs = new RrTRS(2.0);
            MatchStrategy matchStrategy = new NonRandomMs();
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            var ranks = trs.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(30, trs.Matches.Count);
        }

        [TestMethod]
        public void GenerateResultTest_MultipleRoundsPartialRound_2_5()
        {
            var trs = new RrTRS(2.5);
            MatchStrategy matchStrategy = new SimpleRandomMs();
            var competitors = Helpers.CompetitorListHelper.GetStandardCompetitors(6);

            var ranks = trs.GenerateResult(matchStrategy, competitors);

            Assert.AreEqual(36, trs.Matches.Count);
        }
    }
}
