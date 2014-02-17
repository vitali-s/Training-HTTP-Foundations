using System;
using System.Net.Http;
using System.Web.Http.SelfHost;
using HttpFoundations.Bootstrapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpFoundations.Tests.Controllers
{
    [TestClass]
    public abstract class WebApiIntegrationTest
    {
        // To verify traffic on Fiddler please use following UIR: "http://localhost.fiddler:8787/"
        private readonly Uri _baseAddress = new Uri("http://localhost:8787/");

        private HttpSelfHostServer _httpServer;

        [TestInitialize]
        public void Start()
        {
            var configuration = new HttpSelfHostConfiguration(_baseAddress);

            new WebApiApplication().Configure(configuration);

            _httpServer = new HttpSelfHostServer(configuration);

            _httpServer.OpenAsync();
        }

        [TestCleanup]
        public void Stop()
        {
            _httpServer.Dispose();
        }

        public void HttpClientRequest(Action<HttpClient> action)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                action(client);
            }
        }
    }
}