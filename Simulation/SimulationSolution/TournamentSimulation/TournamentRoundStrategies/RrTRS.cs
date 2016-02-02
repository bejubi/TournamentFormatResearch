// TODO: create a tournament round output grid to view a single round robin's results

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.TournamentRoundStrategies
{
    /// <summary>
    /// Generates results for a single full round robin round.
    /// </summary>
    public class RrTRS : TournamentRoundStrategy
    {
        #region Fields and Properties
        private Nullable<double> _numberOfRounds;
        #endregion

        #region Constructors
        public RrTRS() 
        {
            _numberOfRounds = 1.0;
        }

        public RrTRS(double numberOfRounds)
        {
            _numberOfRounds = numberOfRounds;
            WinsToClinchMatch = 1;
        }

        public RrTRS(double numberOfRounds, int winsToClinchMatch)
        {
            _numberOfRounds = numberOfRounds;
            WinsToClinchMatch = winsToClinchMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            if (competitors.Count == 1)
            {
                CompetitorRanks singleRank = new CompetitorRanks();
                singleRank.Add(competitors[0], 1);

                return singleRank;
            }

            List<Match> matches = new List<Match>();

            for (int i = 0; i < Convert.ToInt32(Math.Round(_numberOfRounds.Value)); i++)
            {
                matches.AddRange(GenerateSingleRoundResult(matchStrategy, competitors, 1.0));
            }

            // now run the partial round, if there are any
            double fractionOfPartialRound = (_numberOfRounds.Value - Math.Round(_numberOfRounds.Value));

            if (fractionOfPartialRound > 0.0)
                matches.AddRange(GenerateSingleRoundResult(matchStrategy, competitors, fractionOfPartialRound));

            Matches = matches;

            return GetTournamentRoundRanks();
        }

        private CompetitorRanks GetTournamentRoundRanks()
        {
            CompetitorPoints tournamentRoundPoints = AccumulateMatchPoints(Matches);
            CompetitorRanks tournamentRoundRanks = tournamentRoundPoints.GetCompetitorRanks();

            // break ties until no more are being broken
            while (BreakTies(tournamentRoundRanks, Matches)) { }

            return tournamentRoundRanks;
        }

        // TODO: change the signature to accept a number of additional rounds, rather than the fraction
        private List<Match> GenerateSingleRoundResult(MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors, double fractionOfPartialRound)
        {
            if (fractionOfPartialRound > 1.0 || fractionOfPartialRound < 0 || fractionOfPartialRound == 0)
                throw new ArgumentException("Must be greater than 0.0 and less than or equal to 1.0.", "fractionOfPartialRound");

            List<Match> matches = new List<Match>();

            // The standard algorithm is to write competitor numbers in a 2-row grid with 
            // numbers going clockwise. Then, fixing one of the team numbers in place, rotate the 
            // others around it until done. Match-ups are top and bottom in the column.

            // fill the indexes the first time, including anything to make an even number of indexes
            List<int> competitorIndexes = new List<int>();
            for (int i = 0; i < competitors.Count + (competitors.Count % 2); i++)
            {
                competitorIndexes.Add(i);
            }

            int numberOfRounds = Convert.ToInt32(Math.Round((competitorIndexes.Count - 1) * fractionOfPartialRound));

            // for each round, advance the indexes in the list, except for the last one
            for (int i = 0; i < numberOfRounds; i++)
            {
                // run the matches for the round
                for (int j = 0; j < competitorIndexes.Count / 2; j++)
                {
                    // if the number of competitors is odd, give the first competitor a bye
                    if ((competitors.Count % 2) == 1 && j == 0)
                        continue;

                    // choose the first and last, second and second-to-last, etc.
                    Competitor competitorA = competitors[competitorIndexes[j]];
                    Competitor competitorB = competitors[competitorIndexes[competitorIndexes.Count - j - 1]];

                    Match match = new Match(matchStrategy, WinsToClinchMatch);
                    match.Run(competitorA, competitorB);
                    matches.Add(match);
                }

                // increment the indexes for the next round
                for (int j = 0; j < competitorIndexes.Count - 1; j++)
                {
                    competitorIndexes[j] = (competitorIndexes[j] + 1) % (competitorIndexes.Count - 1);
                }
            }

            return matches;
        }

        private static bool BreakTies(CompetitorRanks tournamentRoundRanks, List<Match> matches)
        {
            bool hasChangedRanks = false;

            List<int> distinctTournamentRoundRanks = tournamentRoundRanks.Values.Distinct().ToList<int>();

            foreach (int tiedRank in distinctTournamentRoundRanks)
            {
                // find all the competitors with the same points
                CompetitorRanks tieGroupRanks = new CompetitorRanks(tournamentRoundRanks
                    .Where(x => x.Value == tiedRank)
                    .ToDictionary(x => x.Key, x => x.Value));

                if (tieGroupRanks.Count() > 1)
                {
                    if (BreakTieGroupByHeadToHeadMatchPoints(tieGroupRanks, matches, 0))
                        hasChangedRanks = true;
                    else if (BreakTieGroupByHeadToHeadMatchPoints(tieGroupRanks, matches, 1))
                        hasChangedRanks = true;
                }

                // add the tie resolution results back into the overall ranks
                foreach (KeyValuePair<Competitor, int> tieGroupRank in tieGroupRanks)
                {
                    tournamentRoundRanks[tieGroupRank.Key] = tieGroupRank.Value;
                }
            }

            return hasChangedRanks;
        }

        private static bool BreakTieGroupByHeadToHeadMatchPoints(CompetitorRanks tieGroupRanks, List<Match> matches, int firstMatchesToExclude)
        {
            bool hasChangedRanks = false;

            // verify there are ties to break
            if (tieGroupRanks.Count() < 2)
                throw new ArgumentException("No tied competitors.");

            // verify all members of the tie group have the same number of points
            int tiedRank = 0;
            foreach (KeyValuePair<Competitor, int> tieMember in tieGroupRanks)
            {
                if (tiedRank != 0 && tiedRank != tieMember.Value)
                    throw new ArgumentException("This group contains members who are not tied with the others.");

                tiedRank = tieMember.Value;
            }

            // compare the points in the head-to-head matches first
            // find all the matches where they go head to head
            List<Match> headToHeadMatches = matches

                .Join(tieGroupRanks,

                match => match.Winner,
                tieMember => tieMember.Key,
                (match, tiedCompetitor) => match)

                .Join(tieGroupRanks,
                match => match.Loser,
                tieMember => tieMember.Key,
                (match, tiedCompetitor) => match)

                .OrderBy(x => x.RunSequence)

                .ToList<Match>();

            for (int i = 0; i < firstMatchesToExclude; i++)
            {
                if (headToHeadMatches.Count > 0)
                    headToHeadMatches.RemoveAt(0);
            }

            CompetitorPoints tiedCompetitorPoints = AccumulateMatchPoints(headToHeadMatches);

            // assign the correct competitor ranks, leaving ties where they exist
            double lastPointTotal = tiedCompetitorPoints.OrderByDescending(x => x.Value).First().Value;
            foreach (KeyValuePair<Competitor, double> tiedCompetitorPoint in tiedCompetitorPoints.OrderByDescending(x => x.Value))
            {
                if (tiedCompetitorPoint.Value < lastPointTotal)
                {
                    tiedRank++;
                    hasChangedRanks = true;
                }

                tieGroupRanks[tiedCompetitorPoint.Key] = tiedRank;

                lastPointTotal = tiedCompetitorPoint.Value;
            }

            return hasChangedRanks;
        }

        protected static new CompetitorPoints AccumulateMatchPoints(List<Match> matches)
        {
            List<Competitor> competitors = matches
                .Select(x => x.Winner)
                .Union(matches.Select(x => x.Loser))
                .Distinct()
                .ToList<Competitor>();

            CompetitorPoints competitorPoints = new CompetitorPoints();
            foreach (Competitor competitor in competitors)
            {
                competitorPoints.Add(competitor, 0.0);
            }

            for (int i = 0; i < competitors.Count - 1; i++)
            {
                for (int j = i + 1; j < competitors.Count; j++)
                {
                    Competitor competitorA = competitors[i];
                    Competitor competitorB = competitors[j];

                    List<Match> aWins = matches
                        .Where(x => (x.Winner == competitorA && x.Loser == competitorB))
                        .ToList<Match>();

                    List<Match> bWins = matches
                        .Where(x => (x.Winner == competitorB && x.Loser == competitorA))
                        .ToList<Match>();

                    if (aWins.Count > 0 || bWins.Count > 0)
                    {
                        competitorPoints[competitorA] += Convert.ToDouble(aWins.Count) / (Convert.ToDouble(aWins.Count) + Convert.ToDouble(bWins.Count));
                        competitorPoints[competitorB] += Convert.ToDouble(bWins.Count) / (Convert.ToDouble(aWins.Count) + Convert.ToDouble(bWins.Count));
                    }
                }
            }

            return competitorPoints;
        }
    }
}
