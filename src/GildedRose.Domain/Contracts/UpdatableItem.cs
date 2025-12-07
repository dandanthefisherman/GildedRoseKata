using GildedRose.Domain.Entities;

namespace GildedRose.Domain.Contracts;

public abstract class UpdatableItem
{
    protected readonly Item Item;

    protected UpdatableItem(Item item)
    {
        Item = item;
    }

    public abstract void Update();
}