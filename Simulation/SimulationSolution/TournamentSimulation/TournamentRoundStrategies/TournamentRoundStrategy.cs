using System.Collections.Generic;
using System.Linq;

using TournamentSimulation.MatchStrategies;
using System;

namespace TournamentSimulation.TournamentRoundStrategies
{
    public abstract class TournamentRoundStrategy
    {
        #region Fields and Properties
        private int _winsToClinchMatch;
        protected int WinsToClinchMatch
        {
            get
            {
                if (_winsToClinchMatch == 0)
                    _winsToClinchMatch = 1;

                return _winsToClinchMatch;
            }
            set
            {
                _winsToClinchMatch = value;
            }
        }

        public List<Match> Matches
        {
            get;
            protected set;
        }
        #endregion

        #region Abstract Methods
        public abstract CompetitorRanks GenerateResult(MatchStrategy matchStrategy, List<Competitor> competitors);
        #endregion

        protected static CompetitorPoints AccumulateMatchPoints(List<Match> matches)
        {
            CompetitorPoints competitorPoints = new CompetitorPoints();

            foreach (Match match in matches)
            {
                if (!competitorPoints.ContainsKey(match.Winner))
                    competitorPoints.Add(match.Winner, 0);

                if (!competitorPoints.ContainsKey(match.Loser))
                    competitorPoints.Add(match.Loser, 0);

                competitorPoints[match.Winner]++;
            }

            return competitorPoints;
        }
    }
}