using System.Threading;
using System.Threading.Tasks;

namespace SvwDesign.HttpClientWrapper
{
    public interface IHttpClientApi
    {
        /// <summary>
        /// Send an asynchronous Get Request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetAsync(string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send an asynchronous Create Request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="requestBody"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> PostAsync<T>(string path, T requestBody, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send an asynchronous Update Request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="requestBody"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> UpdateAsync<T>(string path, T requestBody, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send an asynchronous Delete Request
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> DeleteAsync(string path, CancellationToken cancellationToken = default);
    }
}
