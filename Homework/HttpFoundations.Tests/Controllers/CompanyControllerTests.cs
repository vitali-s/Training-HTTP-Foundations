using System.Net;
using System.Net.Http;
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
            HttpClientRequest(client =>
            {
                client.DefaultRequestHeaders.Add(HttpHeaders.Accept, MediaTypes.ApplicationJson);
                client.DefaultRequestHeaders.Remove(HttpHeaders.AcceptEncoding);

                HttpResponseMessage response = client.GetAsync("company/DefaultCompanyName").Result;

                var companyModel = response.Content.ReadAsAsync<CompanyInfoModel>();

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                Assert.IsFalse(response.Headers.ContainsHeader(HttpHeaders.ContentEncoding));
                Assert.IsNotNull(companyModel);
            });
        }

        [TestMethod]
        public void Company_ShouldBeGZipCompressed_WhenClientSupportGZipCompression()
        {
            HttpClientRequest(client =>
            {
                client.DefaultRequestHeaders.Add(HttpHeaders.Accept, MediaTypes.ApplicationJson);
                client.DefaultRequestHeaders.Add(HttpHeaders.AcceptEncoding, Encodings.GZip);

                HttpResponseMessage response = client.GetAsync("company/DefaultCompanyName").Result;

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                Assert.IsTrue(response.Headers.ContainsHeader(HttpHeaders.ContentEncoding, Encodings.GZip));

                // TODO: check content encoded
            });
        }

        [TestMethod]
        public void Company_ShouldBeDeflateCompressed_WhenClientSupportDeflateCompression()
        {
            HttpClientRequest(client =>
            {
                client.DefaultRequestHeaders.Add(HttpHeaders.Accept, MediaTypes.ApplicationJson);
                client.DefaultRequestHeaders.Add(HttpHeaders.AcceptEncoding, Encodings.Deflate);

                HttpResponseMessage response = client.GetAsync("company/DefaultCompanyName").Result;

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                Assert.IsTrue(response.Headers.ContainsHeader(HttpHeaders.ContentEncoding, Encodings.Deflate));

                // TODO: check content encoded
            });
        }

        [TestMethod]
        public void Company_ShouldBeGZipCompressed_WhenClientSupportGZipAndDeflateCompression()
        {
            HttpClientRequest(client =>
            {
                client.DefaultRequestHeaders.Add(HttpHeaders.Accept, MediaTypes.ApplicationJson);
                client.DefaultRequestHeaders.Add(HttpHeaders.AcceptEncoding, new[] { Encodings.GZip, Encodings.Deflate });

                HttpResponseMessage response = client.GetAsync("company/DefaultCompanyName").Result;

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                Assert.IsTrue(response.Headers.ContainsHeader(HttpHeaders.ContentEncoding, Encodings.GZip));

                // TODO: check content encoded
            });
        }
    }
}
