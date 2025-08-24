using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebDav;

namespace Dav_Connector.Library
{
    public static partial class DavSyncHelper
    {
        public static async Task<List<Byte[]>> GetVCards(String url, String userName, String password)
        {
            var urlUri = new Uri(url);
            var cards = new List<Byte[]>();
            using (var client = new WebDavClient(new WebDavClientParams { BaseAddress = new Uri($"{urlUri.Scheme}://{urlUri.Host}:{urlUri.Port}"), Credentials = new NetworkCredential(userName, password) }))
            {
                var result = await client.Propfind(url, new PropfindParameters());
                foreach (var resource in result.Resources.Where(r => r.ContentType?.Contains("text/vcard") ?? false))
                {
                    try
                    {
                        using (var response = await client.GetFileResponse(new Uri(resource.Uri, UriKind.Relative), false, new GetFileParameters()))
                        {
                            cards.Add(await response.Content.ReadAsByteArrayAsync());
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO
                    }
                }
            }

            return cards;
        }
    }
}
