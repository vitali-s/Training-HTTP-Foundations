using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using HttpFoundations.Constants;
using HttpFoundations.Models;
using HttpFoundations.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpFoundations.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTests : WebApiIntegrationTest
    {
        [TestMethod]
        public void Company_ShouldNotBeCompressed_WhenClientDoNotSupportCompression()
        {
            Company_ShouldBeCompressed_WhenClientSupportCompression();
        }

        [TestMethod]
        public void Company_ShouldBeGZipCompressed_WhenClientSupportGZipCompression()
        {
            Company_ShouldBeCompressed_WhenClientSupportCompression(Encodings.GZip);
        }

        [TestMethod]
        public void Company_ShouldBeDeflateCompressed_WhenClientSupportDeflateCompression()
        {
            Company_ShouldBeCompressed_WhenClientSupportCompression(Encodings.Deflate);
        }

        [TestMethod]
        public void Company_ShouldBeGZipCompressed_WhenClientSupportGZipAndDeflateCompression()
        {
            Company_ShouldBeCompressed_WhenClientSupportCompression(Encodings.GZip, new[] { Encodings.GZip, Encodings.Deflate });
        }

        protected void Company_ShouldBeCompressed_WhenClientSupportCompression(string exceptedEncoding = null, IEnumerable<string> clientEncoding = null)
        {
            HttpClientRequest(client =>
            {
                if (exceptedEncoding != null)
                {
                    client.DefaultRequestHeaders.Add(HttpHeaders.AcceptEncoding, clientEncoding ?? new[] { exceptedEncoding });
                }

                HttpResponseMessage response = client.GetAsync("company/DefaultCompanyName").Result;

                var companyModel = response.Content.ReadJson<CompanyInfoModel>(exceptedEncoding);

                response.StatusCode.Should().Be(HttpStatusCode.OK, "of correct request returns HTTP 200");
                companyModel.Should().NotBeNull("it should contain company information");

                if (exceptedEncoding == null)
                {
                    response.Content.Headers.Should().NotContain(HttpHeaders.ContentEncoding, "of content is not compressed");
                }
                else
                {
                    response.Content.Headers.ContentEncoding.Should().Contain(exceptedEncoding, "of content should compressed using {0}", exceptedEncoding);
                }
            });
        }
    }
}
