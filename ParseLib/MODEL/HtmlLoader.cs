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
            uri = $"{settings.BaseURI}/{settings.Prefix}/";
        }

        public async Task<string> GetSourceFromPage(int id)
        {
            var currentUri = uri.Replace("{CurrentId}", id.ToString());
            var response = await client.GetAsync(currentUri);
            string source = string.Empty;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
