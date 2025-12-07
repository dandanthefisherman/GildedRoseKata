using GildedRose.Application.Interfaces;
using GildedRose.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GildedRose.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGildedRoseApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IItemUpdaterService, ItemUpdaterService>();
        return services;
    }
}
