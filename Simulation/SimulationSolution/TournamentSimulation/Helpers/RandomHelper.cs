using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.Helpers
{
    public static class RandomHelper
    {
        public static List<int> GetRandomSequenceWithoutReplacement(int seed, int maxExclusive)
        {
            Random randomNumberGenerator = new Random(seed); // fixing the seed so we get the same sequence time after time

            List<int> randomSequence = new List<int>();

            while (randomSequence.Count < maxExclusive)
            {
                int randomNumber = randomNumberGenerator.Next(0, maxExclusive);

                if (!randomSequence.Contains(randomNumber))
                    randomSequence.Add(randomNumber);
            }

            return randomSequence;
        }
    }
}
