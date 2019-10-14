using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;
using System.Globalization;
using System.Threading;

namespace ParseTest
{
    class Program
    {
        List<string> Addresses = new List<string>
        {
            "https://www.bestchange.ru/qiwi-to-bitcoin-cash.html",
            "https://www.bestchange.ru/qiwi-to-bitcoin.html",
            "https://www.bestchange.ru/qiwi-to-ethereum.html",
            "https://www.bestchange.ru/qiwi-to-ripple.html",
            "https://www.bestchange.ru/qiwi-to-dogecoin.html"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("\n START");

            var str = "от 100 000.123";

            Console.WriteLine(str);
            Console.ReadKey();

            CultureInfo temp = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-Us");
            Console.WriteLine(Convert.ToDouble(str.Replace(" ", "").Replace("от", "")));
            Thread.CurrentThread.CurrentCulture = temp;
            
            Console.WriteLine(str);
            
            Console.ReadKey();

            //Convert.ToDouble()
            //double.TryParse(str, out double dbl);

            //Console.WriteLine(dbl);


            //var address = "https://www.bestchange.ru/qiwi-to-bitcoin-cash.html";

            //var config = Configuration.Default.WithDefaultLoader();

            //var context = BrowsingContext.New(config);
            //var document = context.OpenAsync(address).Result;

            //var table = document.QuerySelector("#content_table") as IHtmlTableElement;

            //var links = document.Links
            //    .OfType<IHtmlAnchorElement>()
            //    .Select(e => e.Href)
            //    .Where(h => h.Contains("https://www.bestchange.ru/click.php?"));

            //List<BaseExchanger> Table;
            //Console.WriteLine(table is null ? "NULL" : "Table is parsing");

            //table.QuerySelector(TagNames.Thead).Remove();
            //Console.ReadKey();

            //if (table.Rows.Count() == links.Count())
            //{
            //    Console.WriteLine("Count refs equal to count table rows");
            //    Table = new List<BaseExchanger>();
            //    for (int i = 0; i < table.Rows.Count(); i++)
            //    {
            //        BaseExchanger row = new BaseExchanger();
            //        var R = table.Rows[i];
            //        row.ExchangerHref = links.ElementAt(i);
            //        row.ExchangerName = R.QuerySelector(".ca").TextContent;
            //        var sell = R.QuerySelector(".fs");
            //        sell.QuerySelector(TagNames.Small).Remove();                    
            //        row.SellPrice = sell.TextContent;

            //        row.AltcoinName = R.QuerySelector("td:nth-child(4)").QuerySelector(TagNames.Small).TextContent;
            //        row.From = R.QuerySelector(".fm1").TextContent;

            //        if (R.QuerySelectorAll(".fm2").Count() == 1)
            //        {
            //            row.To = R.QuerySelector(".fm2").TextContent;
            //        }
            //        else
            //            row.To = "Infinity";


            //        row.Reserve = R.QuerySelector(".ar.arp").TextContent;
            //        Console.WriteLine("Reserve: " + row.Reserve);
            //        Table.Add(row);
            //    }


            //    Console.ReadKey();
            //    foreach (var row in Table)
            //    {
            //        Console.WriteLine(row.AltcoinName);
            //        Console.WriteLine(row.ExchangerName);
            //        Console.WriteLine(row.ExchangerHref);
            //        Console.WriteLine("Reserve: " + row.Reserve);
            //        Console.WriteLine("Price: " + row.SellPrice);
            //        Console.WriteLine("From: " + row.From + " To: " + row.To);
            //        Console.WriteLine("--- --- --- ---");
            //    }

            //    Console.WriteLine("Total exchangers: " + Table.Count);
            //}
            //else
            //    Console.WriteLine("Count refs not equal to count table rows\nProgram is not correct");
            //Console.ReadKey();



            Console.WriteLine("\n КОНЕЦ!!!");
            Console.ReadKey();
        }
        
        
    }
}
