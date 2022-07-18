// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepartitionTournoi.Domain;
using RepartitionTournoi.Models;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddScoped<ICompositionDomain, CompositionDomain>()
            .AddScoped<ITournoiDomain, TournoiDomain>()
            .AddScoped<IJoueurDomain, JoueurDomain>()
            .RegisterDALServices())
    .Build();
using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

//Init
ICompositionDomain compositionDomain = provider.GetRequiredService<ICompositionDomain>();
IJoueurDomain joueurDomain = provider.GetRequiredService<IJoueurDomain>();

List<Composition> compositions = compositionDomain.GetCompositions();
Console.WriteLine($"Nombre de joueurs = {joueurDomain.All().Count()}, Composition des groupes :");
foreach (Composition composition in compositions)
{
    Console.WriteLine($"Jeu : {composition.Jeu.Nom}");
    foreach (Groupe groupe in composition.Groupes)
    {
        Console.WriteLine($"    {groupe.Nom}");
        Console.WriteLine($"        {string.Join(", ", groupe.Joueurs.Select(x => x.Nom))}");
    }
    Console.WriteLine();
}
Console.WriteLine();
