using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;

namespace ParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n START");
            HttpClient client = new HttpClient();

            string uri = "https://www.bestchange.ru/qiwi-to-bitcoin.html";
            Console.WriteLine("\n 1");
            var source = GetSource(client, uri);
            var domParser = new HtmlParser();
            Console.WriteLine("\n 2");
            var document = GetDocument(domParser, source.Result);

            Console.WriteLine("\n 3");
            var items = document.Result.QuerySelectorAll("div").
                Where(item => item.ClassName != null && item.ClassName.Contains("fs"));

            Console.WriteLine("\n 4");
            int x = 0;
            foreach (var item in items)
            {
                x++;
                Console.WriteLine(item.TextContent);
            }

            Console.WriteLine("total: "+ x);
            Console.WriteLine("\n КОНЕЦ!!!");
            Console.ReadKey();
        }








        static async Task<IHtmlDocument> GetDocument(HtmlParser parser, string source)
        {
            return await parser.ParseDocumentAsync(source);
        }       

        static async Task<string> GetSource(HttpClient client, string uri)
        {
            var response = await client.GetAsync(uri);
            string source = string.Empty;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
