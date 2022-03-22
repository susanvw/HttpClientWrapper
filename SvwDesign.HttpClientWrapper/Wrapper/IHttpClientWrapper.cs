using System.Threading;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper
{
    public interface IHttpClientWrapper
    {
       Task<string> GetAsync(string path, CancellationToken cancellationToken = default);

        Task<string> PostAsync<T>(string path, T content, CancellationToken cancellationToken = default);

        Task<string> UpdateAsync<T>(string path, T content, CancellationToken cancellationToken = default);

        Task<string> DeleteAsync(string path, CancellationToken cancellationToken = default);
    }
}
