using GildedRose.Application.Services;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Tests.ServiceTests;

public class ConjuredItemTests
{
    public static IEnumerable<object[]> ConjuredData =>
        new List<object[]>
        {
            new object[] { 5, 10, 4, 8, "Degrade by 2" },
            new object[] { 0, 10, -1, 6, "Degrade by 4 after expiry" },
            new object[] { 1, 1, 0, 0, "Cannot go below 0" },
            new object[] { -1, 3, -2, 0, "Expired cannot go below 0" }
        };

    [Theory(DisplayName = "Conjured items update correctly")]
    [MemberData(nameof(ConjuredData))]
    public void ItemUpdaterService_Updates_Conjured_Items_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = sellIn, Quality = quality } };
        var updater = new ItemUpdaterService();
        updater.Update(items);

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}