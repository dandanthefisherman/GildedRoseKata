using GildedRose.Domain;
using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.ItemUpdaters;

internal sealed class ConjuredItemUpdater(Item item) : UpdatableItem(item)
{
    public override void Update()
    {
        Item.SellIn--;
        
        int degradeAmount = Item.SellIn < 0 ? 4 : 2;

        Item.Quality -= degradeAmount;

        if (Item.Quality < 0)
            Item.Quality = 0;
    }
}