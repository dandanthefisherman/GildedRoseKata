using GildedRose;

namespace GildedRoseKata;

public class Program
{
    public static void Main(string[] args)
    {
        var updater = ServiceConfigurator.BuildServices();
        
        Console.WriteLine("OMGHAI!");

        var items = InitialItemData.AddItemData();
        
        for (var i = 0; i < 31; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < items.Count; j++)
            {
                System.Console.WriteLine(items[j].Name + ", " + items[j].SellIn + ", " + items[j].Quality);
            }
            Console.WriteLine("");
            updater.Update(items);
        }
    }
}
