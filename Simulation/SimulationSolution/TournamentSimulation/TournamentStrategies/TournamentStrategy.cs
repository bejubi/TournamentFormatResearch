using System.Collections.Generic;
using System.Linq;

using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// Base class for all tournament strategies
    /// </summary>
    public abstract class TournamentStrategy
    {
        #region Abstract Methods
        public abstract CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategy matchStrategy, List<Competitor> competitors);
        public abstract string GetTournamentFormatDescription();
        #endregion

        /// <summary>
        /// Runs through all tournament rounds and accumulates the point totals to the competitors.
        /// </summary>
        /// <param name="tournamentRounds"></param>
        /// <remarks>
        /// This may not be appropriate for all cases, since multiple rounds might not carry over points from previous rounds, or
        /// rank may not be assigned purely on weight of total scores.
        /// </remarks>
        protected static CompetitorPoints AccumulatePointsFromTournamentRounds(List<TournamentRound> tournamentRounds)
        {
            CompetitorPoints competitorPoints = new CompetitorPoints();

            foreach (TournamentRound tournamentRound in tournamentRounds)
            {
                foreach (Match match in tournamentRound.Matches)
                {
                    if (!competitorPoints.ContainsKey(match.Winner))
                        competitorPoints.Add(match.Winner, 0);

                    if (!competitorPoints.ContainsKey(match.Loser))
                        competitorPoints.Add(match.Loser, 0);

                    competitorPoints[match.Winner]++;
                }
            }

            return competitorPoints;
        }
    }
}
