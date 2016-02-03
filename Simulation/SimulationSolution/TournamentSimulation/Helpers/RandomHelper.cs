using System;
using System.Collections.Generic;

namespace TournamentSimulation.Helpers
{
    public static class RandomHelper
    {
        public static List<int> GetRandomSequenceWithoutReplacement(int seed, int maxExclusive)
        {
            var randomNumberGenerator = new Random(seed); // fixing the seed so we get the same sequence time after time

            var randomSequence = new List<int>();

            while (randomSequence.Count < maxExclusive)
            {
                var randomNumber = randomNumberGenerator.Next(0, maxExclusive);

                if (!randomSequence.Contains(randomNumber))
                    randomSequence.Add(randomNumber);
            }

            return randomSequence;
        }
    }
}
