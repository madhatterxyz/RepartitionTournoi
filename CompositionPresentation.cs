using RepartitionTournoi.Domain;
using RepartitionTournoi.Models;

namespace RepartitionTournoi
{
    public class CompositionPresentation : ICompositionPresentation
    {
        private readonly IJoueurDomain _joueurDomain;
        private readonly ICompositionDomain _compositionDomain;
        private List<CompositionDTO> _compositions;
        public CompositionPresentation(IJoueurDomain joueurDomain, ICompositionDomain compositionDomain)
        {
            _joueurDomain = joueurDomain;
            _compositionDomain = compositionDomain;
        }
        public async Task<List<CompositionDTO>> InitCompositions()
        {
            return await _compositionDomain.InitCompositions();
        }
        public async Task DisplayAllCompositions()
        {
            var joueurs = await _joueurDomain.GetAll();
            Console.WriteLine($"Nombre de joueurs = {joueurs.Count()}, Composition des groupes :");
            foreach (CompositionDTO composition in _compositions)
            {
                Console.WriteLine($"Jeu : {composition.Jeu.Nom}");
                foreach (MatchDTO match in composition.Matchs)
                {
                    Console.WriteLine($"    Match {match.Id}");
                    Console.WriteLine($"        {string.Join(", ", match.Scores.Select(x => $"{x.Joueur.Nom} {x.Joueur.Prénom}"))}");
                }
                Console.WriteLine();
            }
        }
        public async Task DisplayInfoParJoueur()
        {
            foreach (JoueurDTO joueur in await _joueurDomain.GetAll())
            {
                Console.WriteLine($"{joueur.Prénom} {joueur.Nom}");
                foreach (CompositionDTO composition in _compositions)
                {
                    Console.WriteLine($"    Jeu {composition.Jeu.Id} : {composition.Jeu.Nom}");
                    foreach (MatchDTO groupe in composition.Matchs.Where(y => y.Scores.Any(z => z.Joueur == joueur)))
                    {
                        Console.WriteLine($"        Tu affronteras les joueurs suivants :");
                        foreach (ScoreDTO adversaire in groupe.Scores.Where(x => x.Joueur != joueur))
                        {
                            Console.WriteLine($"            {adversaire.Joueur.Prénom} {adversaire.Joueur.Nom} {adversaire.Joueur.Téléphone}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        public async Task DisplayScoreBoard()
        {
            Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|", "Prénom", "Nom", "Score"));
            Console.WriteLine("|__________|__________|__________|");
            foreach (JoueurDTO joueur in await _joueurDomain.GetAll())
            {
                int score = _compositions.Sum(x => x.Matchs.Sum(y => y.Scores.Where(z => z.Joueur == joueur).Sum(s => s.Points)));
                Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|", joueur.Prénom, joueur.Nom, score));
            }
        }
    }
}
