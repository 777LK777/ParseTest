using ParseLib.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ParseLib.MODEL
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string uri;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            uri = settings.BaseURI;
        }

        public async Task<string> GetSource()
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
