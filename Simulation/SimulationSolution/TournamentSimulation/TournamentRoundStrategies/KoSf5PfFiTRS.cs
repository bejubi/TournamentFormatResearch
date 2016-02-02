using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentSimulation.TournamentRoundStrategies
{
    /// <summary>
    /// With 5 competitors:
    /// 1 gets a bye into the finals
    /// 2 and 3 fight for the second finals spot, the loser going to petit finals
    /// 4 and 5 fight for the second petit finals spot
    /// </summary>
    public class KoSf5PfFiTRS : TournamentRoundStrategy
    {
        #region Fields and Properties
        private int _winsToClinchSemifinalMatch;
        private int _winsToClinchFinalsMatch;
        private int _winsToClinchPetitMatch;
        #endregion

        #region Constructors
        public KoSf5PfFiTRS(int winsToClinchSemifinalMatch, int winsToClinchFinalsMatch, int winsToClinchPetitMatch)
        {
            _winsToClinchSemifinalMatch = winsToClinchSemifinalMatch;
            _winsToClinchFinalsMatch = winsToClinchFinalsMatch;
            _winsToClinchPetitMatch = winsToClinchFinalsMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            if (competitors.Count != 5)
                throw new ArgumentException("Collection count must be 5.", "competitors");

            List<TournamentRound> tournamentRounds = new List<TournamentRound>();

            // petit final qualifier round
            List<Competitor> petitQualifiers = new List<Competitor>();
            petitQualifiers.Add(competitors[3]);
            petitQualifiers.Add(competitors[4]);

            TournamentRoundStrategy knockoutTRS_petitQualifier = new KoTRS(_winsToClinchSemifinalMatch);
            TournamentRound petitQualifierRound = new TournamentRound(knockoutTRS_petitQualifier, matchStrategy);
            petitQualifierRound.Run(petitQualifiers);
            tournamentRounds.Add(petitQualifierRound);

            // final qualifier round
            List<Competitor> finalsQualifiers = new List<Competitor>();
            finalsQualifiers.Add(competitors[1]);
            finalsQualifiers.Add(competitors[2]);

            TournamentRoundStrategy knockoutTRS_finalQualifier = new KoTRS(_winsToClinchSemifinalMatch);
            TournamentRound finalQualifierRound = new TournamentRound(knockoutTRS_finalQualifier, matchStrategy);
            finalQualifierRound.Run(finalsQualifiers);
            tournamentRounds.Add(finalQualifierRound);

            // petit final round
            List<Competitor> petitFinalsCompetitors = petitQualifierRound.Matches
                .Select(x => x.Winner)
                .Union(finalQualifierRound.Matches
                    .Select(x => x.Loser))
                .ToList<Competitor>();

            TournamentRoundStrategy petitFinalTRS = new KoTRS(_winsToClinchPetitMatch);
            TournamentRound petitFinalRound = new TournamentRound(petitFinalTRS, matchStrategy);
            petitFinalRound.Run(petitFinalsCompetitors);
            tournamentRounds.Add(petitFinalRound);

            // final round
            List<Competitor> finalsCompetitors = finalQualifierRound.Matches
                .Select(x => x.Winner)
                .ToList<Competitor>();

            finalsCompetitors.Add(competitors[0]);

            TournamentRoundStrategy finalTRS = new KoTRS(_winsToClinchFinalsMatch);
            TournamentRound finalRound = new TournamentRound(finalTRS, matchStrategy);
            finalRound.Run(finalsCompetitors);
            tournamentRounds.Add(finalRound);

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

            // assumption: the matches are in this order: match 1 is petit qualifier, 
            // match 2 is final qualifier, match 3 is the petit finals, match 4 is the finals
            CompetitorRanks ranks = new CompetitorRanks();
            ranks.Add(Matches[3].Winner, 1);
            ranks.Add(Matches[3].Loser, 2);
            ranks.Add(Matches[2].Winner, 3);
            ranks.Add(Matches[2].Loser, 4);
            ranks.Add(Matches[0].Loser, 5);

            return ranks;
        }
    }
}