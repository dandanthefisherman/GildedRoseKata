using GildedRose.Domain.Entities;

namespace GildedRose.Application.Interfaces
{
    public interface IItemUpdaterService 
    {
        void Update(List<Item> items);
    }
}