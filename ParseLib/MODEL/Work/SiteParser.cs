using AngleSharp.Html.Dom;
using ParseLib.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using AngleSharp;
using ParseLib.MODEL.Work;
using AngleSharp.Dom;
using System.Globalization;
using System.Threading;
using ParseLib.MODEL;

namespace ParseLib
{
    public class SiteParser : IParser<BaseExchanger[]>
    {
        public virtual BaseExchanger[] Parse(IDocument document)
        {
            var table = document.QuerySelector("#content_table") as IHtmlTableElement;
            var links = document.Links
                .OfType<IHtmlAnchorElement>()
                .Select(e => e.Href)
                .Where(h => h.Contains("https://www.bestchange.ru/click.php?"));

            string time = DateTime.Now.ToLongTimeString();

            Console.WriteLine(table is null ? "NULL" : "Table is parsing");

            table.QuerySelector(TagNames.Thead).Remove();

            if (table.Rows.Count() == links.Count())
            {
                List<BaseExchanger> exchangers = new List<BaseExchanger>();
                Console.WriteLine("Count refs equal to count table rows");
                for (int i = 0; i < table.Rows.Count(); i++)
                {
                    BaseExchanger exchanger = new BaseExchanger
                    {
                        TimeStamp = time
                    };

                    var R = table.Rows[i];

                    exchanger.ExchangerUri = links.ElementAt(i);

                    exchanger.ExchangerName = R.QuerySelector(".ca").TextContent;

                    var sell = R.QuerySelector(".fs");
                    sell.QuerySelector(TagNames.Small).Remove();

                    CultureInfo temp = Thread.CurrentThread.CurrentCulture;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-Us");
                    exchanger.SellPrice = Convert.ToDouble(sell.TextContent.Replace(" ", ""));
                    

                    exchanger.AltcoinName = R.QuerySelector("td:nth-child(4)").QuerySelector(TagNames.Small).TextContent;

                    
                    exchanger.From = Convert.ToDouble(R.QuerySelector(".fm1").TextContent.Replace(" ", "").Replace("от", ""));

                    if (R.QuerySelectorAll(".fm2").Count() == 1)
                    {                        
                        exchanger.To = Convert.ToDouble(R.QuerySelector(".fm2").TextContent.Replace(" ", "").Replace("до", ""));
                    }

                    exchanger.Reserve = Convert.ToDouble(R.QuerySelector(".ar.arp").TextContent.Replace(" ", ""));

                    Thread.CurrentThread.CurrentCulture = temp;
                    exchangers.Add(exchanger);
                }

                return exchangers.ToArray();
            }
            else
            {
                return null;
                throw new InvalidOperationException("Count refs not equal to count table rows\nProgram is not correct");
            }            
        }
    }
}
