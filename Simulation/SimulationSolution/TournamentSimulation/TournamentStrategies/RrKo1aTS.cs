using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// First, a round robin, then a single knockout round between nearby competitors (e.g. 1v2, 2v3, etc)
    /// </summary>
    public class RrKo1aTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRoundRobinRounds;
        private int _winsToClinchKnockoutMatch;
        #endregion

        #region Constructors
        private RrKo1aTS() { }

        public RrKo1aTS(double numberOfRoundRobinRounds, int winsToClinchKnockoutMatch)
        {
            _numberOfRoundRobinRounds = numberOfRoundRobinRounds;
            _winsToClinchKnockoutMatch = winsToClinchKnockoutMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            if (competitors.Count % 2 != 0)
                throw new ArgumentException("Collection count must be even.", "competitors");

            // round robin to seed the semifinals
            TournamentRound rrTR = new TournamentRound(new RrTRS(_numberOfRoundRobinRounds), matchStrategy);
            CompetitorRanks rrRanks = rrTR.Run(competitors);

            List<Competitor> rankedCompetitors = rrRanks.OrderBy(x => x.Value).Select(x => x.Key).ToList<Competitor>();

            CompetitorRanks ranks = new CompetitorRanks();
            // run single knockout round for nearby competitors
            for (int i = 0; i < competitors.Count / 2; i++)
            {
                TournamentRound koTR = new TournamentRound(new KoTRS(_winsToClinchKnockoutMatch), matchStrategy);
                CompetitorRanks koRanks = koTR.Run(new List<Competitor>() { rankedCompetitors[i * 2], rankedCompetitors[(i * 2) + 1] });
                ranks.Add(koRanks.First(x => x.Value == 1).Key, (i * 2) + 1);
                ranks.Add(koRanks.First(x => x.Value == 2).Key, (i * 2) + 2);
            }

            return ranks;
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}); 2) pair nearby competitors and have a single knockout match first to {1} between each pair", _numberOfRoundRobinRounds, _winsToClinchKnockoutMatch);
        }
    }
}
