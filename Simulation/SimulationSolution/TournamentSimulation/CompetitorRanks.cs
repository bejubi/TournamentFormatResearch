using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation
{
    public class CompetitorRanks : Dictionary<Competitor, int>
    {
        public CompetitorRanks() : base() { }

        public CompetitorRanks(Dictionary<Competitor, int> competitorRanksDictionary)
        {
            foreach (KeyValuePair<Competitor, int> competitorRank in competitorRanksDictionary)
            {
                this.Add(competitorRank.Key, competitorRank.Value);
            }
        }
    }
}
