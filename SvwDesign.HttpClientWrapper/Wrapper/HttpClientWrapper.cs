using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper
{

    public class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private bool _disposed;

        private readonly HttpClient _httpClient;
        private readonly IOptions<ServiceBaseOptions> _siteOptions;

        public HttpClientWrapper(IOptions<ServiceBaseOptions> siteOptions)
        {
            _httpClient = new HttpClient();
            _siteOptions = siteOptions;
        }

        public async Task<HttpContent> GetAsync(string path)
        {
            var request = GetRequest(path);
            var response = await _httpClient.SendAsync(request);
            return response.Content;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string path, T request)
        {
            var uri = new Uri($"{_siteOptions.Value.BaseAddress}/{path}");
            var content = GetContent(request);
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> UpdateAsync<T>(string path, T request)
        {
            var uri = new Uri($"{_siteOptions.Value.BaseAddress}/{path}");
            var content = GetContent(request);
            var response = await _httpClient.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private HttpRequestMessage GetRequest(string path)
        {
            var uri = new Uri($"{_siteOptions.Value.BaseAddress}{path}");
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_siteOptions.Value.MediaType));

            return request;
        }
        private HttpContent GetContent<T>(T request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request));
            content.Headers.ContentType = new MediaTypeHeaderValue(_siteOptions.Value.MediaType);
            return content;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }

            _disposed = true;
            _httpClient?.Dispose();
        }
    }
}
