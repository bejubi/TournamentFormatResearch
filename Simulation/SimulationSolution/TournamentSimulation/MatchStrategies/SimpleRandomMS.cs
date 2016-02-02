using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.MatchStrategies
{
    /// <summary>
    /// Hands out the results in a random order based on the ratio of ratings
    /// between competitors.
    /// </summary>
    /// <example>
    /// If CompetitorA.Rating = 40 and CompetitorB.Rating = 60, we would expect
    /// CompetitorA to win 40/100 = 40% of the time
    /// </example>
    public class SimpleRandomMS : MatchStrategy
    {
        // create the randomNumberGenerator statically, so that we're getting a true .Next
        static Random randomNumberGenerator = new Random();

        public override CompetitorRanks GenerateResult(int winsToClinchMatch, Competitor competitorA, Competitor competitorB)
        {
            int competitorAWins = 0;
            int competitorBWins = 0;

            while (competitorAWins < winsToClinchMatch && competitorBWins < winsToClinchMatch)
            {

                double randomRatio = Convert.ToDouble(randomNumberGenerator.Next(0, 101)) / Convert.ToDouble(100);
                double ratingRatio = Convert.ToDouble(competitorA.TheoreticalRating) / (Convert.ToDouble(competitorA.TheoreticalRating) + Convert.ToDouble(competitorB.TheoreticalRating));

                if (randomRatio < ratingRatio)
                    competitorAWins++;
                else
                    competitorBWins++;
            }

            CompetitorRanks ranks = new CompetitorRanks();

            if (competitorAWins > competitorBWins)
            {
                ranks.Add(competitorA, 1);
                ranks.Add(competitorB, 2);
            }
            else
            {
                ranks.Add(competitorB, 1);
                ranks.Add(competitorA, 2);
            }

            return ranks;
        }
    }
}
