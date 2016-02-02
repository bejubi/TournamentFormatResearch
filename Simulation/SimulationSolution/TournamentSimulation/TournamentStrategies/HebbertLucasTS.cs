using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.TournamentStrategies
{
    public class HebbertLucasTS : TournamentStrategy
    {
        public override CompetitorRanks GenerateResult(int tournamentRunSequence, TournamentSimulation.MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            // TODO: implement this strategy
            // reference: http://rulestalk.blogspot.com/2010/10/hebbert-lucas-system.html or http://teamracing.org
            // note: this is basically a round-robin where points are awarded, even if unequal races are sailed

            throw new NotImplementedException();
        }

        public override string GetTournamentFormatDescription()
        {
            throw new NotImplementedException();
        }
    }
}
