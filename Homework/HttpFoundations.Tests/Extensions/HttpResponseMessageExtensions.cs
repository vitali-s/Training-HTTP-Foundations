using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using HttpFoundations.Constants;
using Newtonsoft.Json;

namespace HttpFoundations.Tests.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static TModel ReadJson<TModel>(this HttpContent httpContent, string contentEncoding = null)
        {
            using (var stream = httpContent.ReadAsStreamAsync().Result)
            {
                var contentStream = stream;

                if (string.Compare(contentEncoding, Encodings.GZip, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    contentStream = new GZipStream(stream, CompressionMode.Decompress, false);
                }
                else if (string.Compare(contentEncoding, Encodings.Deflate, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    contentStream = new DeflateStream(stream, CompressionMode.Decompress, false);
                }

                using (contentStream)
                {
                    using (var reader = new StreamReader(contentStream))
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<TModel>(reader.ReadToEnd());
                        }
                        catch
                        {
                            return default(TModel);
                        }
                    }
                }
            }
        }
    }
}
