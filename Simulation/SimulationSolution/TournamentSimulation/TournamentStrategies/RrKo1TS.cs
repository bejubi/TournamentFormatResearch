using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// First, a round robin, then a single knockout round, breaking ties by the round robin results.
    /// </summary>
    public class RrKo1TS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRoundRobinRounds;
        private int _winsToClinchKnockoutMatch;
        #endregion

        #region Constructors
        private RrKo1TS() { }

        public RrKo1TS(double numberOfRoundRobinRounds, int winsToClinchKnockoutMatch)
        {
            _numberOfRoundRobinRounds = numberOfRoundRobinRounds;
            _winsToClinchKnockoutMatch = winsToClinchKnockoutMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            // round robin to seed the semifinals
            TournamentRound rrTR = new TournamentRound(new RrTRS(_numberOfRoundRobinRounds), matchStrategy);
            CompetitorRanks rrRanks = rrTR.Run(competitors);

            // run single semifinal knockout round
            TournamentRound koTR = new TournamentRound(new KoTRS(_winsToClinchKnockoutMatch), matchStrategy);
            CompetitorRanks koRanks = koTR.Run(rrRanks.OrderBy(x => x.Value).Select(x => x.Key).ToList<Competitor>());

            // rank the results first by how they did in the semifinals, then by how they did in the round robin
            var untypedRanks = koRanks.OrderBy(x => x.Value).ThenBy(x => rrRanks[x.Key]);

            CompetitorRanks ranks = new CompetitorRanks();
            int i = 1;
            foreach (KeyValuePair<Competitor, int> untypedRank in untypedRanks)
            {
                ranks.Add(untypedRank.Key, i++);
            }

            return ranks;            
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}); 2) flip competitors and have a single knockout round first to {1}, break ties by round robin rank", _numberOfRoundRobinRounds, _winsToClinchKnockoutMatch);
        }
    }
}
