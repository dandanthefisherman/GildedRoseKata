using GildedRose.Application.Services;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Tests.ServiceTests;

public class BackstagePassItemTests
{
    public static IEnumerable<object[]> BackstageData =>
        new List<object[]>
        {
            new object[] { 15, 20, 14, 21, ">10 days: +1" },
            new object[] { 10, 20, 9, 22, "10 days: +2" },
            new object[] { 6, 20, 5, 22, "6 days: +2" },
            new object[] { 5, 20, 4, 23, "5 days: +3" },
            new object[] { 1, 20, 0, 23, "1 day: +3" },
            new object[] { 0, 20, -1, 0, "Expired: drops to 0" },
            new object[] { 5, 49, 4, 50, "Cannot exceed 50" },
            new object[] { 10, 49, 9, 50, "Cannot exceed 50" },
            new object[] { 15, 50, 14, 50, "Max quality" }
        };

    [Theory(DisplayName = "Backstage passes update correctly")]
    [MemberData(nameof(BackstageData))]
    public void ItemUpdaterService_Updates_Backstage_Passes_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality } };
        var updater = new ItemUpdater();
        updater.Update(items);

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}