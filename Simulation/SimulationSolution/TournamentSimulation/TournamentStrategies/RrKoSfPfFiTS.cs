using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// First, a round robin, then a knockout tournament with semifinals and finals. All ties are kept.
    /// </summary>
    public class RrKoSfPfFiTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRoundRobinRounds;
        private int _winsToClinchKnockoutSemifinalsMatch;
        private int _winsToClinchKnockoutPetitFinalsMatch;
        private int _winsToClinchKnockoutFinalsMatch;
        #endregion

        #region Constructors
        private RrKoSfPfFiTS() { }

        public RrKoSfPfFiTS(double numberOfRoundRobinRounds, int winsToClinchKnockoutSemifinalsMatch, int winsToClinchKnockoutPetitFinalsMatch, int winsToClinchKnockoutFinalsMatch)
        {
            _numberOfRoundRobinRounds = numberOfRoundRobinRounds;
            _winsToClinchKnockoutFinalsMatch = winsToClinchKnockoutFinalsMatch;
            _winsToClinchKnockoutPetitFinalsMatch = winsToClinchKnockoutPetitFinalsMatch;
            _winsToClinchKnockoutSemifinalsMatch = winsToClinchKnockoutSemifinalsMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, TournamentSimulation.MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            // round robin to seed the semifinals
            TournamentRound roundRobinTR = new TournamentRound(new RrTRS(_numberOfRoundRobinRounds), matchStrategy);
            CompetitorRanks rrRanks = roundRobinTR.Run(competitors);

            List<Competitor> rrRankedCompetitors = rrRanks
                .OrderBy(x => x.Value)
                .Select(x => x.Key)
                .ToList<Competitor>();

            List<Competitor> top4 = rrRankedCompetitors
                .Take(4)
                .ToList<Competitor>();

            // semifinals round
            TournamentRound semifinalsTR = new TournamentRound(new KoSfPfFiTRS(_winsToClinchKnockoutSemifinalsMatch, _winsToClinchKnockoutFinalsMatch, _winsToClinchKnockoutPetitFinalsMatch), matchStrategy);
            CompetitorRanks semiRanks = semifinalsTR.Run(top4);

            List<Competitor> bottom4 = rrRankedCompetitors
                .Skip(4)
                .Take(4)
                .ToList<Competitor>();

            // give everyone else 5th
            foreach (Competitor competitor in bottom4)
            {
                semiRanks.Add(competitor, 5);
            }

            return semiRanks;
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}), 2) semifinal first to {1}; 3) final first to {2}, petit final first to {3}, lower than 5th keeps their tie at 5th", _numberOfRoundRobinRounds, _winsToClinchKnockoutSemifinalsMatch, _winsToClinchKnockoutFinalsMatch, _winsToClinchKnockoutPetitFinalsMatch);
        }
    }
}
