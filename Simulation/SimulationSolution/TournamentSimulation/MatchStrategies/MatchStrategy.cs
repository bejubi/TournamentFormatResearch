namespace TournamentSimulation.MatchStrategies
{
    public abstract class MatchStrategy
    {
        public abstract CompetitorRanks GenerateResult(int winsToClinchMatch, Competitor competitorA, Competitor competitorB);
    }
}