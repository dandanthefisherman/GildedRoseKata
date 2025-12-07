using GildedRose.Application.Factories;
using GildedRose.Domain.Contracts;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Services;

public class ItemUpdaterService 
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