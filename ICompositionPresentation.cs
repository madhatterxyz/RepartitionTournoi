using RepartitionTournoi.Models;

namespace RepartitionTournoi
{
    public interface ICompositionPresentation
    {
        Task<List<CompositionDTO>> InitCompositions();
        Task DisplayAllCompositions();
        Task DisplayInfoParJoueur();
        Task DisplayScoreBoard();
    }
}
