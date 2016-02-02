using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public Nullable<int> InitialRank { get; set; }

        private Dictionary<int, int> _tournamentRanks;
        public Dictionary<int, int> TournamentRanks
        {
            get
            {
                if (_tournamentRanks == null)
                    _tournamentRanks = new Dictionary<int, int>();

                return _tournamentRanks;
            }
        }
        #endregion

        public double TournamentRankMean
        {
            get
            {
                double[] tournamentRanks = TournamentRanks
                    .Select(x => (double)x.Value)
                    .ToArray<double>();

                return new Helpers.StatisticsHelper(tournamentRanks).Mean;
            }
        }

        public double TournamentRankStandardDeviation
        {
            get
            {
                double[] tournamentRanks = TournamentRanks
                    .Select(x => (double)x.Value)
                    .ToArray<double>();

                return new Helpers.StatisticsHelper(tournamentRanks).StandardDeviation;
            }
        }

        public Dictionary<int, int> RankFrequencies
        {
            get
            {
                Dictionary<int, int> rankFrequencies = new Dictionary<int, int>();
                foreach (int tournamentRank in TournamentRanks.Values)
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
