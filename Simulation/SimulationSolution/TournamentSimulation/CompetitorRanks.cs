using System.Collections.Generic;

namespace TournamentSimulation
{
    public class CompetitorRanks : Dictionary<Competitor, int>
    {
        public CompetitorRanks() { }

        public CompetitorRanks(Dictionary<Competitor, int> competitorRanksDictionary)
        {
            foreach (var competitorRank in competitorRanksDictionary)
            {
                this.Add(competitorRank.Key, competitorRank.Value);
            }
        }
    }
}
