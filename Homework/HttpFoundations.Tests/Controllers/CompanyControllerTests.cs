using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpFoundations.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTests : WebApiIntegrationTest
    {
        [TestMethod]
        public void Company_ShouldReturnHttpStatusOk_ForDefaultCompanyName()
        {
            HttpClientRequest(client =>
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = client.GetAsync("company/DefaultCompanyName").Result;

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            });
        }
    }
}
