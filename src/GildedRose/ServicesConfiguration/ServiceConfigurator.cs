using Microsoft.Extensions.DependencyInjection;
using GildedRose.Application.Interfaces;
using GildedRose.Extensions;


namespace GildedRose;

public static class ServiceConfigurator
{
    public static IItemUpdaterService BuildServices()
    {
        var services = new ServiceCollection();
        
        services.AddGildedRoseApplicationServices();

        var provider = services.BuildServiceProvider();

        return provider.GetRequiredService<IItemUpdaterService>();
    }
}