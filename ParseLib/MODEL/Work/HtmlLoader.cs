using System;
using AngleSharp.Dom;
using AngleSharp;

namespace ParseLib.MODEL
{
    class HtmlLoader
    {
        readonly IBrowsingContext _context;
        readonly string _address;
        IConfiguration _config;

        IConfiguration Configuration
        {
            get => _config;
            set
            {
                if(value == null)
                {
                    _config = AngleSharp.Configuration.Default.WithDefaultLoader();
                }
                else
                {
                    _config = value;
                }
            }
        }

        public HtmlLoader(Uri address, IConfiguration configuration = null)
        {            
            _address = address.ToString();
            this.Configuration = configuration;

            _context = BrowsingContext.New(this.Configuration);
        }

        public IDocument GetDocument()
        {
            return _context.OpenAsync(_address).Result;            
        }
    }
}
