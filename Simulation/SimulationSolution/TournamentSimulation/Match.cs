using System;
using System.Linq;

using TournamentSimulation.MatchStrategies;

namespace TournamentSimulation
{
    /// <summary>
    /// A single match between competitors.
    /// </summary>
    public class Match
    {
        #region Fields and Properties

        private readonly MatchStrategy matchStrategy;
        private readonly int winsToClinchMatch;
        private static int sequence;

        public int? RunSequence
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

        private Match() { }

        public Match(MatchStrategy matchStrategy, int winsToClinchMatch)
        {
            this.matchStrategy = matchStrategy;
            this.winsToClinchMatch = winsToClinchMatch;
        }

        public void Run(Competitor competitorA, Competitor competitorB)
        {
            RunSequence = ++sequence;

            var ranks = this.matchStrategy.GenerateResult(this.winsToClinchMatch, competitorA, competitorB);

            Winner = ranks.First(x => x.Value == 1).Key;
            Loser = ranks.First(x => x.Value == 2).Key;
        }
    }
}
