using System;
using System.Collections.Generic;

namespace TournamentSimulation.Helpers
{
    public static class CompetitorListHelper
    {
        public static List<Competitor> GetCompetitors(int[] ratings)
        {
            var competitors = new List<Competitor>();

            var name = 'A';
            foreach (var rating in ratings)
            {
                competitors.Add(new Competitor { Name = name.ToString(), TheoreticalRating = rating });
                name++;
            }

            return competitors;
        }

        public static List<Competitor> GetReasonableGuessCompetitors_8()
        {
            var ratings = new[]
            {
                95, // bill hardesty
                50, // nate hollerbach
                45, // steve lowery
                40, // don wilson
                30, // mark johnson
                10, // jen wilson
                6, // robert sansome
                5, // chris wurtz
            };

            return GetCompetitors(ratings);
        }

        public static List<Competitor> GetCompetitorsWithTwoDominants_16()
        {
            var ratings = new[] { 90, 60, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };

            return GetCompetitors(ratings);
        }

        public static List<Competitor> GetCompetitorsWithRandomRatings(int numberOfCompetitors)
        {
            // generate a random sequence of numbers 1 to 100
            var randomRatings = new int[numberOfCompetitors];

            var numberGenerator = new Random();
            for (var i = 0; i < numberOfCompetitors; i++)
            {
                randomRatings[i] = numberGenerator.Next(1, 101);
            }

            return GetCompetitors(randomRatings);
        }

        public static List<Competitor> GetCompetitorsWithRandomizedSeeds(int numberOfCompetitors)
        {
            var orderedCompetitors = GetStandardCompetitors(numberOfCompetitors);

            var randomizedCompetitors = new List<Competitor>();

            foreach (var randomIndex in RandomHelper.GetRandomSequenceWithoutReplacement(1001, orderedCompetitors.Count))
            {
                randomizedCompetitors.Add(orderedCompetitors[randomIndex]);
            }

            return randomizedCompetitors;
        }

        public static List<Competitor> GetStandardCompetitors(int numberOfCompetitors)
        {
            var ratings = new int[numberOfCompetitors];
            var i = 0;
            for (var competitorRating = numberOfCompetitors; competitorRating > 0; competitorRating--)
            {
                ratings[i++] = competitorRating * 10;
            }

            return GetCompetitors(ratings);
        }

        public static List<Competitor> GetEvenlySpacedCompetitors(int numberOfCompetitors)
        {
            var ratings = new int[numberOfCompetitors];

            var competitorRating = 100;
            for (var i = numberOfCompetitors; i > 0; i--)
            {
                ratings[numberOfCompetitors - i] = competitorRating;
                competitorRating = Convert.ToInt32(Math.Round(Convert.ToDouble(competitorRating) * 0.75));
            }

            return GetCompetitors(ratings);
        }
    }
}
