using GildedRose.Domain;
using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.ItemUpdaters;

internal sealed class AgedBrieItemUpdater(Item item) : UpdatableItem(item)
{
    public override void Update()
    {
        Item.SellIn--;

        Item.Quality++;

        if (Item.SellIn < 0)
            Item.Quality++;

        if (Item.Quality > 50)
            Item.Quality = 50;
    }
}