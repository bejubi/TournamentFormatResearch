using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// 1) round robin to seed knockout, 2) semifinals, petit finals, finals knockout for top four, 3) round robin consolation for remainder
    /// </summary>
    /// <remarks>
    /// This is the reference format, since it's most widely used.
    /// </remarks>
    public class RrKoSfPfFiRrTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRoundRobinRounds;
        private int _winsToClinchKnockoutSemifinalsMatch;
        private int _winsToClinchKnockoutPetitFinalsMatch;
        private int _winsToClinchKnockoutFinalsMatch;
        private double _numberOfConsolationRoundRobinRounds;
        #endregion

        #region Constructors
        private RrKoSfPfFiRrTS() { }

        public RrKoSfPfFiRrTS(double numberOfRoundRobinRounds, int winsToClinchKnockoutSemifinalsMatch, int winsToClinchKnockoutPetitFinalsMatch, int winsToClinchKnockoutFinalsMatch, double numberOfConsolationRoundRobinRounds)
        {
            _numberOfRoundRobinRounds = numberOfRoundRobinRounds;
            _winsToClinchKnockoutFinalsMatch = winsToClinchKnockoutFinalsMatch;
            _winsToClinchKnockoutPetitFinalsMatch = winsToClinchKnockoutPetitFinalsMatch;
            _winsToClinchKnockoutSemifinalsMatch = winsToClinchKnockoutSemifinalsMatch;
            _numberOfConsolationRoundRobinRounds = numberOfConsolationRoundRobinRounds;
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

            // single round robin for consolation round
            TournamentRound consolationRR = new TournamentRound(new RrTRS(_numberOfConsolationRoundRobinRounds), matchStrategy);
            CompetitorRanks consolationRanks = consolationRR.Run(bottom4);

            int place = 5;
            foreach (Competitor competitor in consolationRanks.OrderBy(x => x.Value).Select(x => x.Key))
            {
                semiRanks.Add(competitor, place++);
            }

            return semiRanks;
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}) to seed knockout, 2) semifinals first to {1}, 3) petit finals first to {2}, finals knockout first to {3} for top four, 4) round robin ({4}) consolation for remainder", _numberOfRoundRobinRounds, _winsToClinchKnockoutSemifinalsMatch, _winsToClinchKnockoutPetitFinalsMatch, _winsToClinchKnockoutFinalsMatch, _numberOfConsolationRoundRobinRounds);
        }
    }
}
