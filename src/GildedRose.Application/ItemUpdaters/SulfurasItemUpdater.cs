using GildedRose.Domain;
using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.ItemUpdaters;

internal sealed class SulfurasItemUpdater(Item item) : UpdatableItem(item)
{
    public override void Update()
    {
        // nothing updates here
    }
}