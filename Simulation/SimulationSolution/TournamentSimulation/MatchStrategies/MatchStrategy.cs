using System.Collections.Generic;

namespace TournamentSimulation.MatchStrategies
{
    public abstract class MatchStrategy
    {
        #region Abstract Methods
        public abstract CompetitorRanks GenerateResult(int winsToClinchMatch, Competitor competitorA, Competitor competitorB);
        #endregion
    }
}