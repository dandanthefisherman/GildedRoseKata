using GildedRose.Domain.Entities;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTests
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
    public void UpdateQuality_Updates_Standard_Items_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = sellIn, Quality = quality } };
        var app = new GildedRoseKata.GildedRose(items);

        app.UpdateQuality();

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
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
    public void UpdateQuality_Updates_Aged_Brie_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = sellIn, Quality = quality } };
        var app = new GildedRoseKata.GildedRose(items);

        app.UpdateQuality();

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
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
    public void UpdateQuality_Does_Not_Change_Sulfuras(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = sellIn, Quality = quality } };
        var app = new GildedRoseKata.GildedRose(items);

        app.UpdateQuality();

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
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
    public void UpdateQuality_Updates_Backstage_Passes_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality } };
        var app = new GildedRoseKata.GildedRose(items);

        app.UpdateQuality();

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
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
    public void UpdateQuality_Updates_Conjured_Items_Correctly(
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality,
        string _)
    {
        var items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = sellIn, Quality = quality } };
        var app = new GildedRoseKata.GildedRose(items);

        app.UpdateQuality();

        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
    [Fact(DisplayName = "Multiple items update correctly together")]
    public void UpdateQuality_Updates_Multiple_Items_Correctly()
    {
        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Conjured Mana Cake", SellIn = 15, Quality = 20 }
        };

        var app = new GildedRoseKata.GildedRose(items);

        app.UpdateQuality();

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