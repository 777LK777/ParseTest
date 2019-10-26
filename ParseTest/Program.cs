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
        static List<IParserSettings> Addresses = new List<IParserSettings>
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

            ;
            ParserWorker<BaseExchanger[]> worker = new ParserWorker<BaseExchanger[]>(new SiteParser(), Addresses);

            worker.OnNewData += Parser_OnNewData;
            worker.OnCompleted += Parser_OnCompleted;

            
            
            worker.Start();

            

            




            foreach (var i in exchangers)
            {
                Console.WriteLine(i.AltcoinName + "\t" + i.ExchangerName + "\t" + i.ExchangerUri);
            }

            Console.WriteLine("\n END");
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
