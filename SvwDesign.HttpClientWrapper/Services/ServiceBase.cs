using SvwDesign.HttpClientWrapper.Wrapper;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper.Services
{
    public abstract class ServiceBase
    {
        private IHttpClientWrapper HttpClient { get; set; }

        public void Initialize(IHttpClientWrapper httpClientWrapper)
        {
            if (httpClientWrapper == null) throw new ArgumentNullException("httpClientWrapper");
            HttpClient = httpClientWrapper;

            OnInitialize();

        }

       protected abstract void OnInitialize();


        protected async Task<T> ExecuteGetAsync<T>(string action)
        {
            var content = await HttpClient.GetAsync(action);
            var result = await content.ReadAsStringAsync();

            var document = JsonDocument.Parse(result);
            JsonElement element = document.RootElement;

            var json = element.GetRawText();
            var other =  JsonSerializer.Deserialize<T>(json);
            return other;
        }

        protected async Task ExecutePost<T>(string path, T request)
        {
            var response = await HttpClient.PostAsync<T>(path, request);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }
    }
}
