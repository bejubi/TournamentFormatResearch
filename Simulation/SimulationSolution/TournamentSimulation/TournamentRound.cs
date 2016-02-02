using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentRoundStrategies;
using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation
{
    /// <summary>
    /// A single round of a tournament.
    /// </summary>
    public class TournamentRound
    {
        #region Fields and Properties
        private MatchStrategy _matchStrategy;
        private TournamentRoundStrategy _tournamentRoundStrategy;

        private static int _sequence = 0;
        public Nullable<int> RunSequence
        {
            get;
            private set;
        }

        public List<Match> Matches
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        private TournamentRound() { }

        public TournamentRound(TournamentRoundStrategy tournamentRoundStrategy, MatchStrategy matchStrategy)
        {
            _matchStrategy = matchStrategy;
            _tournamentRoundStrategy = tournamentRoundStrategy;
        }
        #endregion

        public CompetitorRanks Run(List<Competitor> competitors)
        {
            RunSequence = ++_sequence;

            CompetitorRanks ranks = _tournamentRoundStrategy.GenerateResult(_matchStrategy, competitors);
            Matches = _tournamentRoundStrategy.Matches;

            return ranks;
        }
    }
}
