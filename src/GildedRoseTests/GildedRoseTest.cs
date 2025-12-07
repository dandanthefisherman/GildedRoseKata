using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTests
{
    [Theory]
    [InlineData("+5 Dexterity Vest", 2, 2, 1, 1)] // before sell date: -1 Quality
    [InlineData("+5 Dexterity Vest", 1, 1, 0, 0)] // quality bottoms out
    [InlineData("+5 Dexterity Vest", 0, 2, -1, 0)] // after sell date: -2 Quality
    [InlineData("+5 Dexterity Vest", 0, 1, -1, 0)] // quality cannot go below zero
    [InlineData("+5 Dexterity Vest", -1, 2, -2, 0)] // already expired: -2 Quality

    public void UpdateQuality_Updates_Standard_Items_Correctly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var items = new List<Item>
        {
            new Item { Name = name, SellIn = sellIn, Quality = quality }
        };

        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
    [Theory]
    [InlineData("Aged Brie", 5, 0, 4, 1)] // before sell date: +1 Quality
    [InlineData("Aged Brie", 1, 49, 0, 50)] // hits quality cap before expiration
    [InlineData("Aged Brie", 0, 48, -1, 50)] // after sell date: +2 but capped at 50
    [InlineData("Aged Brie", 0, 49, -1, 50)] // capped at 50, even at double increase
    [InlineData("Aged Brie", -1, 48, -2, 50)] // already expired: +2 but capped at 50

    public void UpdateQuality_Updates_Aged_Brie_Correctly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var items = new List<Item>
        {
            new Item { Name = name, SellIn = sellIn, Quality = quality }
        };

        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
    [Theory]
    [InlineData("Sulfuras, Hand of Ragnaros", 5, 80, 5, 80)]   // standard case: no changes
    [InlineData("Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]   // on sell date: no changes
    [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, -1, 80)] // after sell date: no changes
    [InlineData("Sulfuras, Hand of Ragnaros", 10, 80, 10, 80)] // always legendary
    public void UpdateQuality_Sulfuras_No_Change_In_Quality(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var items = new List<Item>
        {
            new Item { Name = name, SellIn = sellIn, Quality = quality }
        };

        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
    
    [Theory]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 15, 20, 14, 21)]  // >10 days: +1
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 20, 9, 22)]  // 10 days: +2
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 6, 20, 5, 22)]   // 6 days: +2
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 20, 4, 23)]   // 5 days: +3
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 20, 0, 23)]   // 1 day: +3
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 20, -1, 0)]   // expired: drops to 0
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 49, 4, 50)]   // cannot exceed 50
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 49, 9, 50)]  // cannot exceed 50
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 15, 50, 14, 50)] // max quality
    public void UpdateQuality_Updates_Backstage_Passes_Correctly(
        string name,
        int sellIn,
        int quality,
        int expectedSellIn,
        int expectedQuality)
    {
        // Arrange
        var items = new List<Item>
        {
            new Item { Name = name, SellIn = sellIn, Quality = quality }
        };

        var app = new GildedRose(items);

        // Act
        app.UpdateQuality();

        // Assert
        Assert.Equal(expectedSellIn, items[0].SellIn);
        Assert.Equal(expectedQuality, items[0].Quality);
    }
}