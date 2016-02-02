using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.MatchStrategies
{
    /// <summary>
    /// Simply hands out results in the order the competitors are passed in.
    /// </summary>
    class NonRandomMS : MatchStrategy
    {
        public override CompetitorRanks GenerateResult(int winsToClinchMatch, Competitor competitorA, Competitor competitorB)
        {
            CompetitorRanks ranks = new CompetitorRanks();
            
            if (competitorA.TheoreticalRating > competitorB.TheoreticalRating)
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
