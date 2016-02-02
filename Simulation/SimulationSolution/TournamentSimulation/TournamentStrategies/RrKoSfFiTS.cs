using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    public class RrKoSfFiTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRoundRobinRounds;
        private int _winsToClinchKnockoutMatch;
        #endregion

        #region Constructors
        public RrKoSfFiTS()
        {
            _numberOfRoundRobinRounds = 1.0;
            _winsToClinchKnockoutMatch = 1;
        }

        public RrKoSfFiTS(double numberOfRoundRobinRounds, int winsToClinchKnockoutMatch)
        {
            _numberOfRoundRobinRounds = numberOfRoundRobinRounds;
            _winsToClinchKnockoutMatch = winsToClinchKnockoutMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, TournamentSimulation.MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            // round robin to seed the quarterfinals
            TournamentRound roundRobinTR = new TournamentRound(new RrTRS(_numberOfRoundRobinRounds), matchStrategy);
            CompetitorRanks rrRanks = roundRobinTR.Run(competitors);
            List<Competitor> rr1RankedCompetitors = rrRanks
                .OrderBy(x => x.Value)
                .Select(x => x.Key)
                .ToList<Competitor>();

            // TODO: move all this complexity to a new TournamentRoundStrategy that includes multiple stages of the knockout round quarterfinals through finals
            // knockout through finals
            TournamentRoundStrategy knockoutTRS = new KoTRS(_winsToClinchKnockoutMatch);
            TournamentRound knockoutTR = new TournamentRound(knockoutTRS, matchStrategy);
            knockoutTR.Run(rr1RankedCompetitors);

            List<TournamentRound> knockoutRounds = new List<TournamentRound>();
            knockoutRounds.Add(knockoutTR);
                
            while (knockoutTR.Matches.Count > 1)
            {
                List<Competitor> winningCompetitorsToMoveOn = new List<Competitor>();
                foreach (Match match in knockoutTR.Matches)
                {
                    winningCompetitorsToMoveOn.Add(match.Winner);
                }

                knockoutTR = new TournamentRound(knockoutTRS, matchStrategy);
                knockoutTR.Run(winningCompetitorsToMoveOn);

                knockoutRounds.Add(knockoutTR);
            }

            CompetitorPoints competitorPoints = AccumulatePointsFromTournamentRounds(knockoutRounds);
            CompetitorRanks ranks = competitorPoints.GetCompetitorRanks();

            BreakTiesByRoundRobinRanks(rrRanks, ranks);

            return ranks;
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}), 2)semifinal first to {1}, final knockout first to {1}, break remaining ties by round robin results", _numberOfRoundRobinRounds, _winsToClinchKnockoutMatch);
        }

        private void BreakTiesByRoundRobinRanks(CompetitorRanks roundRobinRanks, CompetitorRanks finalRanks)
        {
            IEnumerable<KeyValuePair<Competitor, int>> orderedRanks = finalRanks
                .OrderBy(x => x.Value)
                .ThenBy(x => roundRobinRanks[x.Key]);

            int rank = 1;
            foreach (KeyValuePair<Competitor, int> orderedRank in orderedRanks)
            {
                finalRanks[orderedRank.Key] = rank++;
            }
        }
    }
}