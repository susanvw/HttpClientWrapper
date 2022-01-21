using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper.Wrapper
{
    public interface IHttpClientWrapper
    {
       Task<HttpContent> GetAsync(string path);

        Task<HttpResponseMessage> PostAsync<T>(string path, T Request);

        Task<HttpResponseMessage> UpdateAsync<T>(string path, T Request);

        Task<HttpResponseMessage> DeleteAsync(Uri requestUri);
    }
}
