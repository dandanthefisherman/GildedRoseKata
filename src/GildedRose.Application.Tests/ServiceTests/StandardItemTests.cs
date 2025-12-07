using GildedRose.Application.Services;
using GildedRose.Domain.Entities;

namespace GildedRose.Application.Tests.ServiceTests;

public class StandardItemTests
{
    public static IEnumerable<object[]> StandardItemData =>
        new List<object[]>
        {
            new object[] { 2, 2, 1, 1, "Before sell date: -1 Quality" },
            new object[] { 1, 1, 0, 0, "Quality bottoms out" },
            new object[] { 0, 2, -1, 0, "After sell date: -2 Quality" },
            new object[] { 0, 1, -1, 0, "Quality cannot go below 0" },
            new object[] { -1, 2, -2, 0, "Already expired: -2 Quality" }
        };

    [Theory(DisplayName = "Standard items update correctly")]
    [MemberData(nameof(StandardItemData))]
    public void ItemUpdaterService_Updates_Standard_Items_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = sellIn, Quality = quality } };
        var updater = new ItemUpdaterService();
        updater.Update(items);

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}