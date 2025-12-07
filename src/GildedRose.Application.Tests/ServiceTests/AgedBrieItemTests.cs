using GildedRose.Application.Services;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Tests.ServiceTests;

public class AgedBrieItemTests
{
    public static IEnumerable<object[]> AgedBrieData =>
        new List<object[]>
        {
            new object[] { 5, 0, 4, 1, "Before sell date: +1 Quality" },
            new object[] { 1, 49, 0, 50, "Hits quality cap before expiration" },
            new object[] { 0, 48, -1, 50, "After sell date: +2 but capped at 50" },
            new object[] { 0, 49, -1, 50, "Capped at 50, even at double increase" },
            new object[] { -1, 48, -2, 50, "Already expired: +2 but capped at 50" }
        };

    [Theory(DisplayName = "Aged Brie updates correctly")]
    [MemberData(nameof(AgedBrieData))]
    public void ItemUpdaterService_Updates_Aged_Brie_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = sellIn, Quality = quality } };
        var updater = new ItemUpdater();
        updater.Update(items);

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}