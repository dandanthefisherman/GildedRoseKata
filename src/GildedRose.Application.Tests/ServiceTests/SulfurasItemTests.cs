using GildedRose.Application.Services;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Tests.ServiceTests;

public class SulfurasItemTests
{
    public static IEnumerable<object[]> SulfurasData =>
        new List<object[]>
        {
            new object[] { 5, 80, 5, 80, "Standard case: no changes" },
            new object[] { 0, 80, 0, 80, "On sell date: no changes" },
            new object[] { -1, 80, -1, 80, "After sell date: no changes" },
            new object[] { 10, 80, 10, 80, "Always legendary" }
        };

    [Theory(DisplayName = "Sulfuras does not change")]
    [MemberData(nameof(SulfurasData))]
    public void ItemUpdaterService_Does_Not_Change_Sulfuras(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = sellIn, Quality = quality } };
        var updater = new ItemUpdater();
        updater.Update(items);

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}