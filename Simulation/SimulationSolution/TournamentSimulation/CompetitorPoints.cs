using System;
using System.Collections.Generic;
using System.Linq;

namespace TournamentSimulation
{
    public class CompetitorPoints : Dictionary<Competitor, double>
    {
        public CompetitorPoints() { }

        public CompetitorPoints(Dictionary<Competitor, double> competitorPointsDictionary)
        {
            foreach (var competitorPoint in competitorPointsDictionary)
            {
                this.Add(competitorPoint.Key, competitorPoint.Value);
            }
        }

        public CompetitorRanks GetCompetitorRanks()
        {
            var tournamentRoundRanks = new CompetitorRanks();

            // sort by highest score and assign places, including ties
            var rank = 1;
            var previousTiedRank = 0;
            double previousScore = 0;

            foreach (var competitorPoints in this.OrderByDescending(x => x.Value))
            {
                if (Math.Round(competitorPoints.Value, 5) == Math.Round(previousScore, 5))
                {
                    tournamentRoundRanks.Add(competitorPoints.Key, previousTiedRank);
                }
                else
                {
                    tournamentRoundRanks.Add(competitorPoints.Key, rank);
                    previousTiedRank = rank;
                }

                previousScore = competitorPoints.Value;

                rank++;
            }

            return tournamentRoundRanks;
        }
    }
}
