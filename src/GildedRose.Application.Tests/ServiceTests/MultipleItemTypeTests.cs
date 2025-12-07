using GildedRose.Application.Services;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Tests.ServiceTests;

public class MultipleItemTypeTests
{
    [Fact(DisplayName = "Multiple items update correctly together")]
    public void ItemUpdaterService_Updates_Multiple_Item_Types_Correctly()
    {
        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Conjured Mana Cake", SellIn = 15, Quality = 20 }
        };

        var updater = new ItemUpdater();
        updater.Update(items);

        // Standard item
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(19, items[0].Quality);

        // Aged Brie
        Assert.Equal(1, items[1].SellIn);
        Assert.Equal(1, items[1].Quality);

        // Sulfuras
        Assert.Equal(0, items[2].SellIn);
        Assert.Equal(80, items[2].Quality);

        // Backstage passes
        Assert.Equal(14, items[3].SellIn);
        Assert.Equal(21, items[3].Quality);
        
        // Conjured Item 
        Assert.Equal(14, items[4].SellIn);
        Assert.Equal(18, items[4].Quality);
    }
}