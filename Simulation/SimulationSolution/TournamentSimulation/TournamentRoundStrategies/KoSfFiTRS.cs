using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.TournamentRoundStrategies
{
    /// <summary>
    /// This strategy simply runs the knockout matches all the way to the finals with the winners moving on.
    /// The losers will tie with one another depending on the level where they last lost
    /// </summary>
    public class KoSfFiTRS : TournamentRoundStrategy
    {
        #region Fields and Properties
        private int _winsToClinchMatch;
        #endregion

        #region Constructors
        public KoSfFiTRS() 
        {
            _winsToClinchMatch = 1;
        }

        public KoSfFiTRS(int winsToClinchMatch)
        {
            _winsToClinchMatch = 1;
        }
        #endregion

        public override CompetitorRanks GenerateResult(TournamentSimulation.MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            TournamentRoundStrategy tournamentRoundStrategy = new KoTRS(_winsToClinchMatch);
            List<TournamentRound> tournamentRounds = new List<TournamentRound>();
            TournamentRound tournamentRound;

            tournamentRound = new TournamentRound(tournamentRoundStrategy, matchStrategy);
            tournamentRound.Run(competitors);
            tournamentRounds.Add(tournamentRound);

            while (tournamentRound.Matches.Count > 1)
            {
                List<Competitor> winners = tournamentRound.Matches
                    .Select(x => x.Winner)
                    .ToList<Competitor>();

                tournamentRound = new TournamentRound(tournamentRoundStrategy, matchStrategy);
                tournamentRound.Run(winners);
                tournamentRounds.Add(tournamentRound);
            }

            List<Match> matches = new List<Match>();
            foreach (TournamentRound round in tournamentRounds)
            {
                matches.AddRange(round.Matches);
            }

            Matches = matches;

            CompetitorPoints competitorPoints = AccumulateMatchPoints(matches);
            return competitorPoints.GetCompetitorRanks();
        }
    }
}
