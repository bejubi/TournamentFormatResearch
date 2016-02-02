using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentStrategies;
using MathNet.Numerics.LinearAlgebra;

namespace TournamentSimulation
{
    public delegate void IteratedEventHandler(object sender, IteratedEventArgs e);

    public class IteratedEventArgs : EventArgs
    {
        public readonly int IterationNumber;
        public IteratedEventArgs(int iterationNumber)
        {
            IterationNumber = iterationNumber;
        }
    }

    /// <summary>
    /// Top-level object that runs several instances of a Tournament.
    /// </summary>
    public class Study
    {
        #region Fields and Properties
        private MatchStrategy _matchStrategy;
        private TournamentStrategy _thisTournamentStrategy;
        private List<Competitor> _competitors;
        private List<Tournament> _tournaments;

        private const int _maxRawTournamentResultsToDisplay = 50;

        private int _currentIteration;

        private List<double> _tournamentMADs;
        private List<double> _tournamentMAD_Adjusteds;
        private List<double> _tournamentMAD_Adjusted_WithOddCompetitorAdjustments;
        private Matrix _combinedTournamentTransformationMatrix;

        private bool _isLowMemoryMode;

        public string TournamentStrategyForDisplay
        {
            get
            {
                string tournamentStrategy = _thisTournamentStrategy.GetType().ToString();
                tournamentStrategy = tournamentStrategy.Remove(0, tournamentStrategy.LastIndexOf('.') + 1);
                tournamentStrategy = tournamentStrategy.Replace("TS", "");

                return string.Format("{0}\r\n\t{1}", tournamentStrategy, _thisTournamentStrategy.GetTournamentFormatDescription());
            }
        }

        public string StrategyInformation
        {
            get
            {
                StringBuilder resultsStringBuilder = new StringBuilder();

                string tournamentStrategy = _thisTournamentStrategy.GetType().ToString();
                tournamentStrategy = tournamentStrategy.Remove(0, tournamentStrategy.LastIndexOf('.') + 1);
                tournamentStrategy = tournamentStrategy.Replace("TS", "");

                string matchStrategy = _matchStrategy.GetType().ToString();
                matchStrategy = matchStrategy.Remove(0, matchStrategy.LastIndexOf('.') + 1);
                matchStrategy = matchStrategy.Replace("MS", "");

                resultsStringBuilder.Append(string.Format("\r\nTournament Strategy: {0}", TournamentStrategyForDisplay));
                resultsStringBuilder.Append(string.Format("\r\nMatch Strategy: {0}", matchStrategy));

                return resultsStringBuilder.ToString();
            }
        }

        public string ResultsStatistics
        {
            get
            {
                StringBuilder resultsStringBuilder = new StringBuilder();

                resultsStringBuilder.Append(string.Format("\r\n\r\nCompetitors: {0}", _competitors.Count));

                //-------------------------------------------------------------------
                double[] tournamentScores;

                resultsStringBuilder.Append("\r\n\r\nAverage Tournament Mean Abosolute Difference (MAD)");

                resultsStringBuilder.Append("\r\n\tall competitors, standardized(mean, std dev):");
                tournamentScores = _tournamentMAD_Adjusteds.ToArray();

                resultsStringBuilder.Append(string.Format("\t{0}\t{1}",
                    Math.Round(new Helpers.StatisticsHelper(tournamentScores).Mean, 3),
                    Math.Round(new Helpers.StatisticsHelper(tournamentScores).StandardDeviation, 3)));

                resultsStringBuilder.Append("\r\n\tall competitors, adjusted for odd(mean, std dev):");
                tournamentScores = _tournamentMAD_Adjusted_WithOddCompetitorAdjustments.ToArray();

                resultsStringBuilder.Append(string.Format("\t{0}\t{1}",
                    Math.Round(new Helpers.StatisticsHelper(tournamentScores).Mean, 3),
                    Math.Round(new Helpers.StatisticsHelper(tournamentScores).StandardDeviation, 3)));

                resultsStringBuilder.Append("\r\n\tall competitors, raw (mean, std dev):");
                tournamentScores = _tournamentMADs.ToArray();

                resultsStringBuilder.Append(string.Format("\t\t{0}\t{1}",
                    Math.Round(new Helpers.StatisticsHelper(tournamentScores).Mean, 3),
                    Math.Round(new Helpers.StatisticsHelper(tournamentScores).StandardDeviation, 3)));

                if (!_isLowMemoryMode)
                {
                    //-------------------------------------------------------------------
                    resultsStringBuilder.Append("\r\n\ttop 2 competitors (mean, std dev):");
                    tournamentScores = _tournaments
                       .Select(x => x.GetTournamentMAD(2))
                       .ToArray<double>();

                    resultsStringBuilder.Append(string.Format("\t\t{0}\t{1}",
                        Math.Round(new Helpers.StatisticsHelper(tournamentScores).Mean, 3),
                        Math.Round(new Helpers.StatisticsHelper(tournamentScores).StandardDeviation, 3)));

                    //-------------------------------------------------------------------
                    resultsStringBuilder.Append("\r\n\ttop 1 competitor (mean, std dev):");
                    tournamentScores = _tournaments
                       .Select(x => x.GetTournamentMAD(1))
                       .ToArray<double>();

                    resultsStringBuilder.Append(string.Format("\t\t{0}\t{1}",
                        Math.Round(new Helpers.StatisticsHelper(tournamentScores).Mean, 3),
                        Math.Round(new Helpers.StatisticsHelper(tournamentScores).StandardDeviation, 3)));
                }

                //-------------------------------------------------------------------
                resultsStringBuilder.Append(string.Format("\r\nCombined transformation matrix adjusted determinant:\t{0}", AdjustedDeterminant));

                return resultsStringBuilder.ToString();
            }
        }

        public double AdjustedDeterminant
        {
            get
            {
                double adjustedDeterminant = CombinedTournamentTransformationMatrix.Determinant();
                adjustedDeterminant = Math.Abs(adjustedDeterminant);
                adjustedDeterminant = Math.Pow(adjustedDeterminant, (1.0 / Convert.ToDouble(_competitors.Count)));
                adjustedDeterminant = Math.Round(adjustedDeterminant, 3);

                return adjustedDeterminant;
            }
        }

        public Matrix CombinedTournamentTransformationMatrix
        {
            get
            {
                Matrix combinedMatrix = _combinedTournamentTransformationMatrix.Clone();

                combinedMatrix.Multiply(1.0 / Convert.ToDouble(_currentIteration));

                return combinedMatrix;
            }
        }

        public string CombinedTournamentTransformationMatrixForDisplay
        {
            get
            {
                StringBuilder resultsStringBuilder = new StringBuilder();
                Matrix displayMatrix = CombinedTournamentTransformationMatrix.Clone();
                displayMatrix.Multiply(100.0);

                resultsStringBuilder.Append(string.Format("\r\n\r\nCombined transformation matrix:\r\n{0}", GetMatrixOutput(displayMatrix)));

                return resultsStringBuilder.ToString();
            }
        }

        public string CompetitorInformation
        {
            get
            {
                StringBuilder resultsStringBuilder = new StringBuilder();

                resultsStringBuilder.Append("\r\n\r\nCompetitors (rating, initial rank, tournament rank mean and std. dev.):");
                _competitors.Sort((x, y) => y.TheoreticalRating.CompareTo(x.TheoreticalRating));

                foreach (Competitor competitor in _competitors)
                {
                    string initialRank;
                    if (competitor.InitialRank != null)
                        initialRank = competitor.InitialRank.ToString();
                    else
                        initialRank = "-";

                    resultsStringBuilder.Append(string.Format("\r\n{0}:\t{1}\t{2}\t{3}\t{4}",
                        competitor.Name, competitor.TheoreticalRating, initialRank, Math.Round(competitor.TournamentRankMean, 2), Math.Round(competitor.TournamentRankStandardDeviation, 2)));
                }

                return resultsStringBuilder.ToString();
            }
        }

        public string ResultsFrequenciesGraph
        {
            get
            {
                StringBuilder displayResults = new StringBuilder();

                displayResults.Append("\r\n\r\nRank Frequencies:");

                foreach (Competitor competitor in _competitors)
                {
                    string initialRank;
                    if (competitor.InitialRank != null)
                        initialRank = competitor.InitialRank.ToString();
                    else
                        initialRank = "-";

                    displayResults.Append(string.Format("\r\n\r\n{0}:", competitor.Name));

                    // first, fill in the results gaps
                    Dictionary<int, int> rankFrequencies = new Dictionary<int, int>();
                    int lastFrequency = 0;
                    foreach (KeyValuePair<int, int> rankFrequency in competitor.RankFrequencies.OrderBy(x => x.Key))
                    {
                        for (int i = lastFrequency + 1; i < rankFrequency.Key; i++)
                        {
                            rankFrequencies.Add(i, 0);
                        }

                        rankFrequencies.Add(rankFrequency.Key, rankFrequency.Value);
                        lastFrequency = rankFrequency.Key;
                    }

                    for (int i = lastFrequency + 1; i < _competitors.Count + 1; i++)
                    {
                        rankFrequencies.Add(i, 0);
                    }

                    // create a graph of asterisks
                    foreach (KeyValuePair<int, int> rankFrequency in rankFrequencies.OrderBy(x => x.Key))
                    {
                        double frequencyPercentage = Math.Round(Convert.ToDouble(rankFrequency.Value) / Convert.ToDouble(_currentIteration) * 100);

                        displayResults.Append(string.Format("\r\n{0} {1}\t", rankFrequency.Key, new string('*', Convert.ToInt32(frequencyPercentage))));
                    }
                }

                return displayResults.ToString();
            }
        }

        public string RawTournamentResultsForDisplay
        {
            get
            {
                StringBuilder displayResults = new StringBuilder();

                displayResults.Append("\r\n\r\nRaw Results:");

                _competitors.Sort((x, y) => y.TheoreticalRating.CompareTo(x.TheoreticalRating));

                foreach (Competitor competitor in _competitors)
                {
                    displayResults.Append("\r\n");
                    displayResults.Append(string.Format("{0}, ", competitor.Name));

                    int i = 0;
                    foreach (KeyValuePair<int, int> tournamentRank in competitor.TournamentRanks.OrderByDescending(x => x.Key))
                    {
                        displayResults.Append(string.Format("{0}, ", tournamentRank.Value));

                        i++;

                        if (i > _maxRawTournamentResultsToDisplay)
                        {
                            displayResults.Append("...");
                            break;
                        }
                    }

                    // remove the last comma
                    if (i <= _maxRawTournamentResultsToDisplay)
                        displayResults.Remove(displayResults.Length - 2, 2);
                }

                return displayResults.ToString();
            }
        }

        #endregion

        #region Constructors
        private Study() { }

        public Study(TournamentStrategy tournamentStrategy, MatchStrategy matchStrategy, bool isLowMemoryMode)
        {
            _matchStrategy = matchStrategy;
            _thisTournamentStrategy = tournamentStrategy;
            _isLowMemoryMode = isLowMemoryMode;

            if (!_isLowMemoryMode)
                _tournaments = new List<Tournament>();

            _tournamentMADs = new List<double>();
            _tournamentMAD_Adjusteds = new List<double>();
            _tournamentMAD_Adjusted_WithOddCompetitorAdjustments = new List<double>();

        }
        #endregion

        public event IteratedEventHandler Iterated;

        protected virtual void OnIterated(EventArgs e)
        {
            if (Iterated != null)
            {
                _currentIteration++;

                if (_currentIteration % 1000 == 0)
                    Iterated(this, new IteratedEventArgs(_currentIteration));
            }
        }

        public void Run(List<TournamentSimulation.Competitor> competitors, int numberOfIterations)
        {
            Run(competitors, numberOfIterations, false);
        }

        public void Run(List<TournamentSimulation.Competitor> competitors, int numberOfIterations, bool isJumbleCompetitorSeeds)
        {
            // TODO: look at reducing memory usage by storing results after the run and destroying the actual tournament runs
            _competitors = competitors;
            _combinedTournamentTransformationMatrix = new Matrix(_competitors.Count, _competitors.Count);

            if (!isJumbleCompetitorSeeds)
            {
                int rank = 1;
                foreach (Competitor competitor in _competitors)
                {
                    if (competitor.InitialRank == null)
                        competitor.InitialRank = rank++;
                }
            }

            // run the tournaments
            for (int i = 0; i < numberOfIterations; i++)
            {
                OnIterated(EventArgs.Empty);

                Tournament tournament = new Tournament(_thisTournamentStrategy, _matchStrategy);

                List<Competitor> tournamentCompetitors;

                if (isJumbleCompetitorSeeds)
                    tournamentCompetitors = JumbleCompetitors(i, competitors);
                else
                    tournamentCompetitors = competitors;

                tournament.Run(tournamentCompetitors);

                _tournamentMADs.Add(tournament.TournamentMAD);
                _tournamentMAD_Adjusteds.Add(tournament.TournamentMAD_Standardized);
                _tournamentMAD_Adjusted_WithOddCompetitorAdjustments.Add(tournament.TournamentMAD_Standardized_WithOddCompetitorAdjustment);
                _combinedTournamentTransformationMatrix += tournament.TournamentTransformationMatrix;

                if (!_isLowMemoryMode)
                    _tournaments.Add(tournament);
            }
        }

        // TODO: change the rating structure to randomly assign ratings, to see how sensitve the models are to changes
        // TODO: change the rating partway through and test against second rating
        // TODO: check to make sure frequencies for each rank add up correctly

        private List<Competitor> JumbleCompetitors(int randomSeed, List<Competitor> competitors)
        {
            List<Competitor> randomizedCompetitors = new List<Competitor>();

            foreach (int randomIndex in Helpers.RandomHelper.GetRandomSequenceWithoutReplacement(randomSeed, competitors.Count))
            {
                randomizedCompetitors.Add(competitors[randomIndex]);
            }

            return randomizedCompetitors;
        }

        private string GetMatrixOutput(Matrix matrix)
        {
            StringBuilder matrixPrint = new StringBuilder();

            for (int i = 0; i < matrix.RowCount; i++)
            {
                if (i > 0)
                    matrixPrint.Append("\r\n");

                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    if (j > 0)
                        matrixPrint.Append("  ");

                    matrixPrint.Append(matrix[i, j].ToString("00.00"));
                }
            }

            return matrixPrint.ToString();
        }
    }
}
