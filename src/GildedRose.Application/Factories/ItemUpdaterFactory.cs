using GildedRose.Application.ItemUpdaters;
using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Factories;

internal static class ItemUpdaterFactory
{
    public static UpdatableItem Update(Item item)
    {
        return item.Name switch
        {
            "Aged Brie" => new AgedBrieItemUpdater(item),
            "Backstage passes to a TAFKAL80ETC concert" => new BackstagePassItemUpdater(item),
            "Sulfuras, Hand of Ragnaros" => new SulfurasItemUpdater(item),
            "Conjured Mana Cake" => new ConjuredItemUpdater(item),
            _ => new StandardItemUpdater(item)
        };
    }
}