using System;
using System.Collections.Generic;
using System.Linq;

namespace TournamentSimulation
{
    /// <summary>
    /// A competitor in a tournament.
    /// </summary>
    public class Competitor
    {
        #region Fields and Properties
        
        public string Name { get; set; }
        public int TheoreticalRating {get; set; }
        public int? InitialRank { get; set; }

        private Dictionary<int, int> _tournamentRanks;
        public Dictionary<int, int> TournamentRanks
        {
            get
            {
                return this._tournamentRanks ?? (this._tournamentRanks = new Dictionary<int, int>());
            }
        }

        #endregion

        public double TournamentRankMean
        {
            get
            {
                var tournamentRanks = TournamentRanks
                    .Select(x => (double)x.Value)
                    .ToArray();

                return new Helpers.StatisticsHelper(tournamentRanks).Mean;
            }
        }

        public double TournamentRankStandardDeviation
        {
            get
            {
                var tournamentRanks = TournamentRanks
                    .Select(x => (double)x.Value)
                    .ToArray();

                return new Helpers.StatisticsHelper(tournamentRanks).StandardDeviation;
            }
        }

        public Dictionary<int, int> RankFrequencies
        {
            get
            {
                var rankFrequencies = new Dictionary<int, int>();
                foreach (var tournamentRank in TournamentRanks.Values)
                {
                    if (!rankFrequencies.ContainsKey(tournamentRank))
                        rankFrequencies.Add(tournamentRank, 0);

                    rankFrequencies[tournamentRank]++;
                }

                return rankFrequencies;
            }
        }
    }
}
