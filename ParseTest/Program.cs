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
using ParseLib;
using ParseLib.MODEL.Work;
using ParseLib.Model;

namespace ParseTest
{
    class Program
    {
        static List<SiteSettings> Addresses = new List<SiteSettings>
        {
            new SiteSettings("https://www.bestchange.ru/qiwi-to-bitcoin-cash.html"),
            new SiteSettings("https://www.bestchange.ru/qiwi-to-bitcoin.html"),
            new SiteSettings("https://www.bestchange.ru/qiwi-to-ethereum.html"),
            new SiteSettings("https://www.bestchange.ru/qiwi-to-ripple.html"),
            new SiteSettings("https://www.bestchange.ru/qiwi-to-dogecoin.html")
        };

        static List<BaseExchanger> exchangers = new List<BaseExchanger>();

        static void Main(string[] args)
        {
            Console.WriteLine("\n START");
            
            ParserWorker<BaseExchanger[]> worker = new ParserWorker<BaseExchanger[]>();

            worker.OnNewData += Parser_OnNewData;
            worker.OnCompleted += Parser_OnCompleted;

            Addresses.ForEach( addr =>
            {
                worker.Settings = addr;
                worker.Parser = new SiteParser();
                worker.Start();
            });

            Console.ReadKey();
        }

        private static void Parser_OnNewData(object arg1, BaseExchanger[] arg2)
        {
            exchangers.AddRange(arg2);
        }

        private static void Parser_OnCompleted(object obj)
        {
            Console.WriteLine("Распарсили");
            Console.WriteLine("Count: " + exchangers.Count);
        }
    }
}



//Console.WriteLine(Addresses[0]);

//List<BaseSiteParser> parsers = new List<BaseSiteParser>();

//foreach(var address in Addresses)
//{
//    parsers.Add(new BaseSiteParser(address));
//}

//List<BaseExchanger> exchangers = new List<BaseExchanger>();

//foreach(var parser in parsers)
//{
//    exchangers.AddRange(parser.Parse());
//}

//foreach (var result in exchangers)
//{
//    Console.WriteLine(result.AltcoinName + "\t" + result.SellPrice + "\t" + result.ExchangerName + "\tFrom: " + result.From + "\tTo: " + result.To + "\tReserve: " + result.Reserve);
//    Console.WriteLine(result.TimeOfReceipt + "\t" + result.ExchangerUri);
//    Console.WriteLine("--- --- --- ---");
//}
