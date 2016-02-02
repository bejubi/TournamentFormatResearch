using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.TournamentRoundStrategies
{
    /// <summary>
    /// This strategy runs a 4-competitor tournament round resulting in semis,
    /// leading to finals and petit finals.
    /// </summary>
    public class KoSfPfFiTRS : TournamentRoundStrategy
    {
        #region Fields and Properties
        private int _winsToClinchSemifinalMatch;
        private int _winsToClinchFinalsMatch;
        private int _winsToClinchPetitMatch;
        #endregion

        #region Constructors
        public KoSfPfFiTRS(int winsToClinchSemifinalMatch, int winsToClinchFinalsMatch, int winsToClinchPetitMatch)
        {
            _winsToClinchSemifinalMatch = winsToClinchSemifinalMatch;
            _winsToClinchFinalsMatch = winsToClinchFinalsMatch;
            _winsToClinchPetitMatch = winsToClinchFinalsMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(TournamentSimulation.MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            if (competitors.Count != 4)
                throw new ArgumentException("Collection count must be 4.", "competitors");

            List<TournamentRound> tournamentRounds = new List<TournamentRound>();

            // semifinal round
            TournamentRoundStrategy semifinalTRS = new KoTRS(_winsToClinchSemifinalMatch);
            TournamentRound semifinalRound = new TournamentRound(semifinalTRS, matchStrategy);
            semifinalRound.Run(competitors);
            tournamentRounds.Add(semifinalRound);

            // petit final round
            List<Competitor> losers = semifinalRound.Matches
                .Select(x => x.Loser)
                .ToList<Competitor>();

            TournamentRoundStrategy petitFinalTRS = new KoTRS(_winsToClinchPetitMatch);
            TournamentRound petitFinalRound = new TournamentRound(petitFinalTRS, matchStrategy);
            petitFinalRound.Run(losers);
            tournamentRounds.Add(petitFinalRound);

            // final round
            List<Competitor> winners = semifinalRound.Matches
                .Select(x => x.Winner)
                .ToList<Competitor>();

            TournamentRoundStrategy finalTRS = new KoTRS(_winsToClinchFinalsMatch);
            TournamentRound finalRound = new TournamentRound(finalTRS, matchStrategy);
            finalRound.Run(winners);
            tournamentRounds.Add(finalRound);

            // return the matches that were run
            // return the matches that were run
            List<Match> matches = tournamentRounds
                .SelectMany(x => x.Matches)
                .Select(x => x)
                .ToList<Match>();

            Matches = matches;

            return GetTournamentRoundRanks();
        }

        private CompetitorRanks GetTournamentRoundRanks()
        {
            if (Matches.Count != 4)
                throw new ArgumentException("Collection count must be 4.", "matches");

            // assumption: the matches are in this order: matches 1 and 2 are semis,
            // match 3 is petit finals, match 4 is finals
            CompetitorRanks ranks = new CompetitorRanks();
            ranks.Add(Matches[3].Winner, 1);
            ranks.Add(Matches[3].Loser, 2);
            ranks.Add(Matches[2].Winner, 3);
            ranks.Add(Matches[2].Loser, 4);

            return ranks;
        }
    }
}
