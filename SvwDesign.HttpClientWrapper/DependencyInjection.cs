using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace SvwDesign.HttpClientWrapper
{
    public static class DependencyInjection
    {
        public static void AddHttpClientWrapperModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var section = configuration.GetSection(HttpClientWrapperOptions.ConfigSection);
            services.Configure<HttpClientWrapperOptions>(section); 

            var baseAddres = section["BaseAddress"]; 
            services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>(client =>
            {
                client.BaseAddress = new Uri(baseAddres);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(15))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        }



        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(10));
        }
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));
        }
    }
}
