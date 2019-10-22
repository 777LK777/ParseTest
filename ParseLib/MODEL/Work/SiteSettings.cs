using ParseLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.MODEL.Work
{
    public class SiteSettings : IParserSettings
    {
        public Uri BaseURI { get; set; }     
        
        public SiteSettings(string Address)
        {
            this.BaseURI = new Uri(Address);
        }
    }
}
