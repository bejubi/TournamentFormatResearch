using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentStrategies;
using MathNet.Numerics.LinearAlgebra;

namespace TournamentSimulation
{
    /// <summary>
    /// Runs tournament rounds according to a Tournament Strategy.
    /// </summary>
    public class Tournament
    {
        #region Fields and Properties
        private MatchStrategy _matchStrategy;
        private TournamentStrategy _tournamentStrategy;
        private List<Competitor> _competitors;
        
        private static int _sequence = 0;
        public Nullable<int> RunSequence
        {
            get;
            private set;
        }

        private CompetitorRanks _competitorTournamentRanks;

        public string TournamentFormatDescription
        {
            get
            {
                return _tournamentStrategy.GetTournamentFormatDescription();
            }
        }

        /// <summary>
        /// Computes the mean absolute difference between the expected and the observed tournament result.
        /// </summary>
        public double TournamentMAD
        {
            get
            {
                return GetTournamentMAD(_competitors);
            }
        }

        /// <summary>
        /// Computes the mean absolute difference between the expected and the observed tournament result,
        /// but adjusts for the number of competitors.
        /// </summary>
        /// 
        public double TournamentMAD_Standardized
        {
            get
            {
                double tournamentMAD = TournamentMAD;

                // adjust for the variability due to the number of competitors by dividing by the number of competitors
                // discussion: the slope of max MAD to number of competitors is 1/2 (adjusted for the odd-number issue mentioned above)
                tournamentMAD = tournamentMAD / _competitors.Count;

                // adjust to normalize with the max at 100 (max after the above adjustments is .5 for each n)
                tournamentMAD = tournamentMAD * 200;

                return tournamentMAD;
            }
        }

        /// <summary>
        /// Computes the mean absolute difference between the expected and the observed tournament result,
        /// but adjusts for the number of competitors.
        /// </summary>
        /// 
        public double TournamentMAD_Standardized_WithOddCompetitorAdjustment
        {
            get
            {
                double tournamentMAD = TournamentMAD_Standardized;

                // adjust for odd numbers of competitors to "normalize the data"
                // discussion: For even competitors, the max MAD is n/2. For odd competitors, the max MAD converges to n/2
                // but is thrown off by a denominator that's too large by 1, because the "center" is always 0
                if (_competitors.Count % 2 != 0)
                    tournamentMAD = tournamentMAD * (Math.Pow(_competitors.Count, 2) / (Math.Pow(_competitors.Count, 2) - 1));

                return tournamentMAD;
            }
        }

        public Matrix TournamentTransformationMatrix
        {
            get
            {
                CompetitorPoints theoreticalRatings = new CompetitorPoints();
                foreach (Competitor competitor in _competitors)
                {
                    theoreticalRatings.Add(competitor, competitor.TheoreticalRating);
                }

                CompetitorRanks theoreticalRanks = theoreticalRatings.GetCompetitorRanks();

                Matrix transformationMatrix = new Matrix(_competitors.Count, _competitors.Count);

                foreach (KeyValuePair<Competitor, int> theoreticalRank in theoreticalRanks)
                {
                    int startingRank = theoreticalRank.Value;
                    int finalRank = theoreticalRank.Key.TournamentRanks[RunSequence.Value];

                    transformationMatrix[startingRank - 1, finalRank - 1] = 1.0;
                }

                return transformationMatrix;
            }
        }
        #endregion

        #region Constructors
        private Tournament() { }

        public Tournament(TournamentStrategy tournamentStrategy, MatchStrategy matchStrategy)
        {
            _matchStrategy = matchStrategy;
            _tournamentStrategy = tournamentStrategy;
        }
        #endregion

        public void Run(List<Competitor> competitors)
        {
            RunSequence = ++_sequence;

            _competitors = competitors;

            _competitorTournamentRanks = _tournamentStrategy.GenerateResult(RunSequence.Value, _matchStrategy, competitors);

            AssignTournamentRanksToCompetitorObjects();
        }

        /// <summary>
        /// Computes the mean absolute difference between the expected and the observed tournament result.
        /// </summary>
        public double GetTournamentMAD(int topNumberOfCompetitors)
        {
            List<Competitor> topCompetitors = _competitors
                .OrderByDescending(x => x.TheoreticalRating)
                .Take(topNumberOfCompetitors)
                .ToList<Competitor>();

            return GetTournamentMAD(topCompetitors);
        }

        /// <summary>
        /// Computes the mean absolute difference between the expected and the observed tournament result.
        /// </summary>
        /// <param name="competitors"></param>
        /// <returns></returns>
        public double GetTournamentMAD(List<Competitor> competitors)
        {
            CompetitorPoints theoreticalRatings = new CompetitorPoints();
            foreach (Competitor competitor in competitors)
            {
                theoreticalRatings.Add(competitor, competitor.TheoreticalRating);
            }

            CompetitorRanks theoreticalRanks = theoreticalRatings.GetCompetitorRanks();

            // TODO: convert to using the StatisticsHelper or Iridium
            double[] competitorDiffs = theoreticalRanks
                .Select(x => Math.Abs((double)x.Value - (double)x.Key.TournamentRanks[RunSequence.Value]))
                .ToArray<double>();

            return new Helpers.StatisticsHelper(competitorDiffs).Mean;
        }

        /// <summary>
        /// Assigns tournament ranks to the Competitor.TournamentRanks object. This should be called at the end
        /// of each tournament run.
        /// </summary>
        private void AssignTournamentRanksToCompetitorObjects()
        {
            foreach (KeyValuePair<Competitor, int> rank in _competitorTournamentRanks)
            {
                rank.Key.TournamentRanks.Add(RunSequence.Value, rank.Value);
            }
        }
    }
}