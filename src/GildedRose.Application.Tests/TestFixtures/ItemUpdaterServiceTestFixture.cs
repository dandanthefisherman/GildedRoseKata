using Microsoft.Extensions.DependencyInjection;
using GildedRose.Application.Interfaces;
using GildedRose.Extensions;

namespace GildedRose.Application.Tests.TestFixtures;

public class ItemUpdaterServiceTestFixture
{
    private ServiceProvider Provider { get; }
    public IItemUpdaterService ItemUpdater => Provider.GetRequiredService<IItemUpdaterService>();

    public ItemUpdaterServiceTestFixture()
    {
        var services = new ServiceCollection();
        
        services.AddGildedRoseApplicationServices();

        Provider = services.BuildServiceProvider();
    }
}