using GildedRose.Application.Factories;
using GildedRose.Application.Interfaces;
using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Services;

public class ItemUpdaterService : IItemUpdaterService
{
    public void Update(List<Item> items)
    {
        foreach (var item in items)
        {
            UpdatableItem updater = ItemUpdaterFactory.Update(item);
            updater.Update();
        }
    }
}