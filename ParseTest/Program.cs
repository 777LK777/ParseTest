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

namespace ParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n START");
            HttpClient client = new HttpClient();

            /*
            string uri = "https://www.bestchange.ru/qiwi-to-bitcoin.html";
            Console.WriteLine("\n 1");
            */

            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.bestchange.ru/qiwi-to-bitcoin.html";
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(address).Result;

            var table = document.QuerySelector("#content_table") as IHtmlTableElement;
            
            var links = document.Links
                .OfType<IHtmlAnchorElement>()
                .Select(e => e.Href)
                .Where(h => h.Contains("https://www.bestchange.ru/click.php?"));

            List<MyInfoRow> Table;
            Console.WriteLine(table is null ? "NULL" : "Table is parsing");

            table.QuerySelector(TagNames.Thead).Remove();
            Console.ReadKey();

            if (table.Rows.Count() == links.Count())
            {
                Console.WriteLine("Count refs equal to count table rows");
                Table = new List<MyInfoRow>();
                for (int i = 0; i < table.Rows.Count(); i++)
                {
                    MyInfoRow row = new MyInfoRow();
                    var R = table.Rows[i];
                    row.ExchangerHref = links.ElementAt(i);
                    row.ExchangerName = R.QuerySelector(".ca").TextContent;
                    var sell = R.QuerySelector(".fs");
                    sell.QuerySelector(TagNames.Small).Remove();                    
                    row.SellPrice = sell.TextContent;
                    row.AltcoinName = "BTC";
                    row.From = R.QuerySelector(".fm1").TextContent;

                    
                    //var foo = R.QuerySelector(".fm").GetElementsByClassName("fm2")..SingleOrDefault();
                    //Console.WriteLine("FOO: " + row.ExchangerName + "::" + foo.TextContent);

                    var tos = R.QuerySelectorAll(".fm2");

                    if (tos.Count() == 1)
                    {
                        row.To = tos.ElementAt(0).TextContent;
                    }
                    else
                        row.To = "Infinity";


                    // "#content_table > tbody > tr:nth-child(11) > td.ar.arp"
                    row.Reserve = R.QuerySelector(".ar.arp").TextContent;
                    Console.WriteLine("Reserve: " + row.Reserve);
                    Table.Add(row);
                }

                //"#content_table > tbody > tr.last > td:nth-child(3) > div.fm > div.fm2"
                Console.ReadKey();
                foreach (var row in Table)
                {
                    Console.WriteLine(row.AltcoinName);
                    Console.WriteLine(row.ExchangerName);
                    Console.WriteLine(row.ExchangerHref);
                    Console.WriteLine("Reserve: " + row.Reserve);
                    Console.WriteLine("Price: " + row.SellPrice);
                    Console.WriteLine("From: " + row.From + " To: " + row.To);
                    Console.WriteLine("--- --- --- ---");
                }

                Console.WriteLine("Total exchangers: " + Table.Count);
            }
            else
                Console.WriteLine("Count refs not equal to count table rows\nProgram is not correct");
            Console.ReadKey();

            
            

/*


            foreach (var element in table.QuerySelectorAll("small"))
            {
                if(element.TextContent != "BTC")
                    element.Remove();
            }

            foreach (var element in table.QuerySelectorAll(".fm1"))
            {
                Console.WriteLine(element);
            }            

            Console.ReadKey();

            

            int cnt1 = 0;
            foreach (var link in links)
            {
                Console.WriteLine("Ссыль: " + link);
                cnt1++;
            }
            Console.WriteLine("Total: " + cnt1);
            Console.ReadKey();


            
            int x = 0;
            foreach(var row in table.Rows)
            {
                Console.WriteLine(row.TextContent);
                Console.WriteLine("--- --- --- ---");
                Console.ReadKey();
                x++;
            }
            Console.WriteLine("total: " + x);
            Console.WriteLine("count: " + table.Rows.Count());
            */

            /*
            var domParser = new HtmlParser();
            Console.WriteLine("\n 2");
            var document = GetDocument(domParser, source);

           

            Console.WriteLine("\n 3");


            var items = document.Result.QuerySelectorAll("tr").Where(item => item.ClassName != null && item.Attributes.Contains("onclick"));

            //var itemsSmall = document.Result.QuerySelectorAll("small");

            Console.WriteLine("\n 4");
            int x = 0;
            foreach (var item in items)
            {
                if (item is IHtmlTableRowElement newItem)
                {
                    foreach(var cell in newItem.Cells)
                    {
                        Console.WriteLine(cell.TextContent);
                    }
                }
                Console.WriteLine("\n--- --- --- ---");
                x++;
            }

            Console.WriteLine("total: " + x);
            */


            Console.WriteLine("\n КОНЕЦ!!!");
            Console.ReadKey();
        }

        //static async Task<IDocument> GetDocument(IBrowsingContext document, string address)
        //{
        //    return await document.OpenAsync(address);
        //}


        static async Task<IHtmlDocument> GetDocument(HtmlParser parser, string source)
        {
            var res = await parser.ParseDocumentAsync(source);           

            return res;
        }

        static async Task<IDocument> GetDocument(IBrowsingContext context, string source)
        {
            var document = await context.OpenAsync(req => req.Content(source));

            return document;
        }






        static async Task<string> GetSource(HttpClient client, string uri)
        {
            var response = await client.GetAsync(uri);
            string source = string.Empty;

            

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            if(source == null)
            {
                Console.WriteLine("String is empty");
            }

            return source;
        }
    }
}
