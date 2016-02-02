using System;
using System.Collections.Generic;

using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation.TournamentRoundStrategies
{
    /// <summary>
    /// Generates results for a single knockout tournament round.
    /// </summary>
    public class KoTRS : TournamentRoundStrategy
    {
        private KoTRS() { }

        public KoTRS(int winsToClinchMatch)
        {
            WinsToClinchMatch = winsToClinchMatch;
        }

        public override CompetitorRanks GenerateResult(MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            List<Match> matches = new List<Match>();

            if (competitors.Count % 2 != 0)
                throw new ArgumentException("Collection count must be even.", "competitors");

            // generate the results for the competitors
            // note that the competitors are paired in this round by the order they're in in the List object
            // pairing first with last, second with second-to-last, etc.
            for (int index = 0; index < competitors.Count / 2; index++)
            {
                int mirrorIndex = competitors.Count - (index + 1);

                Competitor competitorA = competitors[index];
                Competitor competitorB = competitors[mirrorIndex];

                Match match = new Match(matchStrategy, WinsToClinchMatch);
                match.Run(competitorA, competitorB);
                matches.Add(match);
            }

            Matches = matches;

            CompetitorPoints tournamentRoundPoints = AccumulateMatchPoints(matches);
            return tournamentRoundPoints.GetCompetitorRanks();
        }
    }
}
