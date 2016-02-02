using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TournamentSimulation.TournamentStrategies;
using TournamentSimulation.TournamentRoundStrategies;

namespace TournamentSimulation.TournamentStrategies
{
    /// <summary>
    /// Hans Graf's tournament strategy:
    /// RR1 or 2 to seed 1-4 into RR and 5-8 into RR. RRA 1, 2 go to finals. RRA3 3, RRB 1 go to petit finals, all others to a consolation RR
    /// </summary>
    public class RrRrSf5KoFiPfTS : TournamentStrategy
    {
        #region Fields and Properties
        private double _numberOfRr1Rounds;
        private double _numberOfRr2Rounds;
        private double _numberOfRr3Rounds;
        private int _winsToClinchFinalMatch;
        private int _winsToClinchPetitFinalMatch;
        #endregion

        #region Constructors
        private RrRrSf5KoFiPfTS() { }

        public RrRrSf5KoFiPfTS(double numberOfRr1Rounds, double numberOfRr2Rounds, int winsToClinchFinalMatch, int winsToClinchPetitFinalMatch, double numberOfRr3Rounds)
        {
            _numberOfRr1Rounds = numberOfRr1Rounds;
            _numberOfRr2Rounds = numberOfRr2Rounds;
            _numberOfRr3Rounds = numberOfRr3Rounds;
            _winsToClinchFinalMatch = winsToClinchFinalMatch;
            _winsToClinchPetitFinalMatch = winsToClinchPetitFinalMatch;
        }
        #endregion

        public override CompetitorRanks GenerateResult(int tournamentRunSequence, MatchStrategies.MatchStrategy matchStrategy, List<Competitor> competitors)
        {
            if (competitors.Count != 8)
                throw new ArgumentException("Collection count must be 8.", "competitors");

            // round robin to seed 2 separate round robins
            TournamentRound rr1 = new TournamentRound(new RrTRS(_numberOfRr1Rounds), matchStrategy);
            CompetitorRanks rr1Ranks = rr1.Run(competitors);

            List<Competitor> rrACompetitors = rr1Ranks
                .OrderBy(x => x.Value)
                .Take(4)
                .Select(x => x.Key)
                .ToList<Competitor>();

            List<Competitor> rrBCompetitors = rr1Ranks
                .OrderBy(x => x.Value)
                .Skip(4)
                .Take(4)
                .Select(x => x.Key)
                .ToList<Competitor>();

            // round robin to determine 1, 2, 3
            TournamentRound rrA = new TournamentRound(new RrTRS(_numberOfRr2Rounds), matchStrategy);
            CompetitorRanks rrARanks = rrA.Run(rrACompetitors);

            // round robin to determine 4
            TournamentRound rrB = new TournamentRound(new RrTRS(_numberOfRr2Rounds), matchStrategy);
            CompetitorRanks rrBRanks = rrB.Run(rrBCompetitors);

            // finals
            List<Competitor> finalsCompetitors = rrARanks
                .OrderBy(x => x.Value)
                .Take(2)
                .Select(x => x.Key)
                .ToList<Competitor>();

            TournamentRound finals = new TournamentRound(new KoTRS(_winsToClinchFinalMatch), matchStrategy);
            CompetitorRanks finalsRanks = finals.Run(finalsCompetitors);

            // petit finals
            List<Competitor> petitFinalsCompetitors = rrARanks
                .OrderBy(x => x.Value)
                .Skip(2)
                .Take(1)
                .Select(x => x.Key)
                .ToList<Competitor>();

            Competitor rrBWinner = rrBRanks
                .OrderBy(x => x.Value)
                .Take(1)
                .Select(x => x.Key)
                .First();

            petitFinalsCompetitors.Add(rrBWinner);

            TournamentRound petitFinals = new TournamentRound(new KoTRS(_winsToClinchPetitFinalMatch), matchStrategy);
            CompetitorRanks petitFinalsRanks = petitFinals.Run(petitFinalsCompetitors);

            // consolation round
            List<Competitor> consolationCompetitors = rrBRanks
                .OrderBy(x => x.Value)
                .Skip(1)
                .Select(x => x.Key)
                .ToList<Competitor>();

            Competitor rrALoser = rrARanks
                .OrderBy(x => x.Value)
                .Skip(3)
                .Take(1)
                .Select(x => x.Key)
                .First();

            consolationCompetitors.Add(rrALoser);

            TournamentRound consolationRound = new TournamentRound(new RrTRS(_numberOfRr3Rounds), matchStrategy);
            CompetitorRanks consolationRanks = consolationRound.Run(consolationCompetitors);

            CompetitorRanks ranks = new CompetitorRanks();
            
            int i = 1;
            foreach (var rank in finalsRanks.OrderBy(x => x.Value))
            {
                ranks.Add(rank.Key, i++);
            }

            foreach (var rank in petitFinalsRanks.OrderBy(x => x.Value))
            {
                ranks.Add(rank.Key, i++);
            }

            foreach (var rank in consolationRanks.OrderBy(x => x.Value))
            {
                ranks.Add(rank.Key, i++);
            }

            return ranks;
        }

        public override string GetTournamentFormatDescription()
        {
            return string.Format("1) round robin ({0}); 2) seed 1-4 into RRA ({1}) and 5-8 into RRB ({1}); 3) RRA 1, 2 go to finals, RRA 3, RRB 1 go to petit finals, all others to a consolation RR; 4) finals first to {2}, petit finals first to {3}, consolation round robin ({4})", _numberOfRr1Rounds, _numberOfRr2Rounds, _winsToClinchFinalMatch, _winsToClinchPetitFinalMatch, _numberOfRr3Rounds);
        }
    }
}
