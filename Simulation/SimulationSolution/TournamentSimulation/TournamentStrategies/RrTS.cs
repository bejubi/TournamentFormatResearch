using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.MatchStrategies;
using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    public class RrTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRounds;
        private int _winsToClinchMatch;
        #endregion

        #region Constructors
        public RrTS()
        {
            _numberOfRounds = 1.0;
            _winsToClinchMatch = 1;
        }

        public RrTS(double numberOfRounds)
        {
            _numberOfRounds = numberOfRounds;
            _winsToClinchMatch = 1;
        }

        public RrTS(double numberOfRounds, int winsToClinchMatch)
        {
            _numberOfRounds = numberOfRounds;
            _winsToClinchMatch = winsToClinchMatch;
            
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            TournamentRound tournamentRound = new TournamentRound(new RrTRS(_numberOfRounds, _winsToClinchMatch), matchStrategy);
            return tournamentRound.Run(competitors);
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}) first to {1}", _numberOfRounds, _winsToClinchMatch);
        }
    }
}
