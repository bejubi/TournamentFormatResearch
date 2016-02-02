using System.Collections.Generic;
using System.Linq;

using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    public class KoTS : TournamentStrategy
    {
        #region Fields and Properties
        private int _winsToClinchMatch;
        #endregion

        #region Constructors
        public KoTS()
        {
            _winsToClinchMatch = 1;
        }

        public KoTS(int winsToClinchMatch)
        {
            _winsToClinchMatch = winsToClinchMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            TournamentRoundStrategy tournamentRoundStrategy = new KoSfFiTRS(_winsToClinchMatch);

            TournamentRound tournamentRound = new TournamentRound(tournamentRoundStrategy, matchStrategy);
            
            return tournamentRound.Run(competitors);
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) knockout semifinal first to {0}, final knockout first to {0}, keep all remaining ties", _winsToClinchMatch);
        }
    }
}