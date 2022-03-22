using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper
{

    public class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private bool _disposed;

        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string path, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Get, path, cancellationToken);
        }


        public async Task<string> PostAsync<T>(string path, T content,  CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Post, content, path, cancellationToken);
        }

        public async Task<string> UpdateAsync<T>(string path, T content, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Put, content, path, cancellationToken);
        }

        public async Task<string> DeleteAsync(string path, CancellationToken cancellationToken = default)
        {
            return await SendRequestAsync(HttpMethod.Delete, path, cancellationToken);
        }


        private async Task<string> SendRequestAsync(HttpMethod method, string path, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(method, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }


        private async Task<string> SendRequestAsync<T>(HttpMethod method, T body,  string path, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(method, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
