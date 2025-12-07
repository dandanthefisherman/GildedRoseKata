using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.ItemUpdaters;

internal sealed class BackstagePassItemUpdater(Item item) : UpdatableItem(item)
{
    private const int MaxQuality = 50;

    public override void Update()
    {
        Item.SellIn--;

        if (Item.SellIn < 0)
        {
            Item.Quality = 0;
            return;
        }

        int increaseAmount = Item.SellIn < 5 ? 3 :
            Item.SellIn < 10 ? 2 : 1;

        Item.Quality = Item.Quality + increaseAmount > MaxQuality 
            ? MaxQuality 
            : Item.Quality + increaseAmount;
    }
}