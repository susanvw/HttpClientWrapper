using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper
{

    public class HttpClientApi : IHttpClientApi
    {


        private readonly HttpClient _httpClient;

        public HttpClientApi(HttpClient httpClient)
        {
            //_httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(siteOptions.Value.BaseAddress)
            //};
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string path, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Get, path, cancellationToken);
        }


        public async Task<string> DeleteAsync(string path, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Delete, path, cancellationToken);
        }

        public async Task<string> PostAsync<T>(string path, T requestBody, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Post, path, requestBody, cancellationToken);
        }

        public async Task<string> UpdateAsync<T>(string path, T requestBody, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Put, path, requestBody, cancellationToken);
        }


        private async Task<string> SendRequestAsync(HttpMethod method, string path, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(method, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        private async Task<string> SendRequestAsync<T>(HttpMethod method, string path, T requestBody, CancellationToken cancellationToken)
        {
            var content = JsonSerializer.Serialize(requestBody);

            var request = new HttpRequestMessage(method, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(content, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}
