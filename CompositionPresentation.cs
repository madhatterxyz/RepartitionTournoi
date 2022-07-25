using RepartitionTournoi.Domain;
using RepartitionTournoi.Models;

namespace RepartitionTournoi
{
    public class CompositionPresentation : ICompositionPresentation
    {
        private readonly IJoueurDomain _joueurDomain;
        private readonly List<Composition> _compositions;
        public CompositionPresentation(IJoueurDomain joueurDomain, ICompositionDomain compositionDomain)
        {
            _joueurDomain = joueurDomain;
            _compositions = compositionDomain.GetCompositions();
        }
        public void DisplayAllCompositions()
        {
            Console.WriteLine($"Nombre de joueurs = {_joueurDomain.All().Count()}, Composition des groupes :");
            foreach (Composition composition in _compositions)
            {
                Console.WriteLine($"Jeu : {composition.Jeu.Nom}");
                foreach (Match match in composition.Matchs)
                {
                    Console.WriteLine($"    Match {match.Id}");
                    Console.WriteLine($"        {string.Join(", ", match.Scores.Select(x => $"{x.Joueur.Nom} {x.Joueur.Prénom}"))}");
                }
                Console.WriteLine();
            }
        }
        public void DisplayInfoParJoueur()
        {
            foreach (Joueur joueur in _joueurDomain.All())
            {
                Console.WriteLine($"{joueur.Prénom} {joueur.Nom}");
                foreach (Composition composition in _compositions)
                {
                    Console.WriteLine($"    Jeu {composition.Jeu.Id} : {composition.Jeu.Nom}");
                    foreach (Match groupe in composition.Matchs.Where(y => y.Scores.Any(z => z.Joueur == joueur)))
                    {
                        Console.WriteLine($"        Tu affronteras les joueurs suivants :");
                        foreach (Score adversaire in groupe.Scores.Where(x => x.Joueur != joueur))
                        {
                            Console.WriteLine($"            {adversaire.Joueur.Prénom} {adversaire.Joueur.Nom} {adversaire.Joueur.Téléphone}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        public void DisplayScoreBoard()
        {
            Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|", "Prénom", "Nom", "Score"));
            Console.WriteLine("|__________|__________|__________|");
            foreach (Joueur joueur in _joueurDomain.All())
            {
                int score = _compositions.Sum(x => x.Matchs.Sum(y => y.Scores.Where(z => z.Joueur == joueur).Sum(s => s.Points)));
                Console.WriteLine(String.Format("|{0,10}|{1,10}|{2,10}|", joueur.Prénom, joueur.Nom, score)); 
            }
        }
    }
}
