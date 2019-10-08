using ParseLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.MODEL.Work
{
    class SiteSettings : IParserSettings
    {
        public string BaseURI { get; set; } = "https://habrahabr.ru";
        public string Prefix { get; set; } = "page";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
