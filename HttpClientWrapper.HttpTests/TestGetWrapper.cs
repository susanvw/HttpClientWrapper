using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SvwDesign.HttpClientWrapper;
using System.Net.Http;

namespace HttpClientWrapper.HttpTests
{
    public class Tests
    {
        private HttpClient _wrapper;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.test.json")
                   .AddEnvironmentVariables()
                   .Build();

            var Services = new ServiceCollection();
            Services.AddHttpClientWrapperModule(config);

            // Build the intermediate service provider
            var sp = Services.BuildServiceProvider();

            var factory = (IHttpClientFactory)sp.GetRequiredService(typeof(IHttpClientFactory));

            var section = config.GetSection(HttpClientWrapperOptions.ConfigSection);
            var baseAddres = section["BaseAddress"];

            _wrapper = factory.CreateClient();
            _wrapper.BaseAddress = new System.Uri(baseAddres);
        }

        [Test]
        public void GetCatJoke()
        {
            var items = _wrapper.GetAsync("fact").Result;
            Assert.IsTrue(items is not null);
        }
    }
}