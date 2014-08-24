using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace HttpFoundations.Tests.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static bool ContainsHeader(this HttpResponseHeaders responseHeaders, string key, string value = null)
        {
            var httpHeader = responseHeaders.FirstOrDefault(header => string.Compare(header.Key, key, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (httpHeader.Equals(default(KeyValuePair<string, IEnumerable<string>>)))
            {
                return false;
            }

            if (value != null)
            {
                var httpHeaderValue = httpHeader.Value.FirstOrDefault(headerValue => string.Compare(headerValue, value, StringComparison.InvariantCultureIgnoreCase) == 0);

                if (httpHeaderValue == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
