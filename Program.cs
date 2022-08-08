// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepartitionTournoi;
using RepartitionTournoi.Domain;
using RepartitionTournoi.Domain.Interfaces;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddSingleton<ICompositionPresentation, CompositionPresentation>()
            .AddSingleton<ICompositionDomain, CompositionDomain>()
            .AddSingleton<ITournoiDomain, TournoiDomain>()
            .AddSingleton<IJoueurDomain, JoueurDomain>()
            .RegisterDALServices())
    .Build();
using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;


//Init
ICompositionPresentation compositionPresentation = provider.GetRequiredService<ICompositionPresentation>();

string entry = string.Empty;
do
{
    Console.WriteLine("Quelle fonction affichée? ");
    Console.WriteLine(" A = Toutes les compositions");
    Console.WriteLine(" B = Les informations à envoyer aux joueurs");
    Console.WriteLine(" C = Les scores des joueurs");
    entry = Console.ReadLine();
    switch (entry)
    {
        case "A":
            compositionPresentation.DisplayAllCompositions();
            break;
        case "B":
            compositionPresentation.DisplayInfoParJoueur();
            break;
        case "C":
            compositionPresentation.DisplayScoreBoard();
            break;
    }

}
while (!string.IsNullOrEmpty(entry));

Console.WriteLine();
