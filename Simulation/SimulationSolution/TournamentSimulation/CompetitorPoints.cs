using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation
{
    public class CompetitorPoints : Dictionary<Competitor, double>
    {
        public CompetitorPoints() : base() { }

        public CompetitorPoints(Dictionary<Competitor, double> competitorPointsDictionary)
        {
            foreach (KeyValuePair<Competitor, double> competitorPoint in competitorPointsDictionary)
            {
                this.Add(competitorPoint.Key, competitorPoint.Value);
            }
        }

        public CompetitorRanks GetCompetitorRanks()
        {
            CompetitorRanks tournamentRoundRanks = new CompetitorRanks();

            // sort by highest score and assign places, including ties
            int rank = 1;
            int previousTiedRank = 0;
            double previousScore = 0;

            foreach (KeyValuePair<Competitor, double> competitorPoints in this.OrderByDescending(x => x.Value))
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
