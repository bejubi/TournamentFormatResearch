using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// Round robin to seed the semis, then a 5-boat semis, with 1st getting a bye into finals
    /// </summary>
    public class RrKoSf5PfFiTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRoundRobinRounds;
        private int _winsToClinchKnockoutSemifinalsMatch;
        private int _winsToClinchKnockoutPetitFinalsMatch;
        private int _winsToClinchKnockoutFinalsMatch;
        #endregion

        #region Constructors
        private RrKoSf5PfFiTS() { }

        public RrKoSf5PfFiTS(double numberOfRoundRobinRounds, int winsToClinchKnockoutSemifinalsMatch, int winsToClinchKnockoutPetitFinalsMatch, int winsToClinchKnockoutFinalsMatch)
        {
            _numberOfRoundRobinRounds = numberOfRoundRobinRounds;
            _winsToClinchKnockoutFinalsMatch = winsToClinchKnockoutFinalsMatch;
            _winsToClinchKnockoutPetitFinalsMatch = winsToClinchKnockoutPetitFinalsMatch;
            _winsToClinchKnockoutSemifinalsMatch = winsToClinchKnockoutSemifinalsMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            // round robin to seed the semifinals
            TournamentRound roundRobinTR = new TournamentRound(new RrTRS(_numberOfRoundRobinRounds), matchStrategy);
            CompetitorRanks rrRanks = roundRobinTR.Run(competitors);

            List<Competitor> rrRankedCompetitors = rrRanks
                .OrderBy(x => x.Value)
                .Select(x => x.Key)
                .ToList<Competitor>();

            // semifinals round
            TournamentRound semifinals5TR = new TournamentRound(new KoSf5PfFiTRS(_winsToClinchKnockoutSemifinalsMatch, _winsToClinchKnockoutFinalsMatch, _winsToClinchKnockoutPetitFinalsMatch), matchStrategy);
            
            return semifinals5TR.Run(rrRankedCompetitors);
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}); 2) 1st place gets a bye to the finals; 3) semifinals first to {1} 2nd & 3rd compete for 2nd spot in finals and 1st spot in petit finals, 4th & 5th compete for 2nd spot in petit finals; 4) finals first to {2}, petit finals first to {3}", _numberOfRoundRobinRounds, _winsToClinchKnockoutSemifinalsMatch, _winsToClinchKnockoutFinalsMatch, _winsToClinchKnockoutPetitFinalsMatch);
        }
    }
}
