using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.TournamentStrategies
{
    public class RandomTS : TournamentStrategy
    {
        private static Random _randomPointsGenerator = new Random();

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            CompetitorPoints randomPoints = new CompetitorPoints();
            foreach (Competitor competitor in competitors)
            {
                randomPoints.Add(competitor, _randomPointsGenerator.Next(1, 1000));
            }

            return randomPoints.GetCompetitorRanks();
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("Uniform random assignment of all tournament results.");
        }
    }
}
