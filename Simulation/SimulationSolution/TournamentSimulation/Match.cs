using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation
{
    /// <summary>
    /// A single match between competitors.
    /// </summary>
    public class Match
    {
        #region Fields and Properties
        private MatchStrategy _matchStrategy;
        private int _winsToClinchMatch;
        private static int _sequence = 0;

        public Nullable<int> RunSequence
        {
            get;
            private set;
        }

        public Competitor Winner
        {
            get;
            private set;
        }

        public Competitor Loser
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        private Match() { }

        public Match(MatchStrategy matchStrategy, int winsToClinchMatch)
        {
            _matchStrategy = matchStrategy;
            _winsToClinchMatch = winsToClinchMatch;
        }
        #endregion

        public void Run(Competitor competitorA, Competitor competitorB)
        {
            RunSequence = ++_sequence;

            CompetitorRanks ranks = _matchStrategy.GenerateResult(_winsToClinchMatch, competitorA, competitorB);

            Winner = ranks.First(x => x.Value == 1).Key;
            Loser = ranks.First(x => x.Value == 2).Key;
        }
    }
}
