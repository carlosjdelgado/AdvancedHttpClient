using AdvancedHttpClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedHttpClient
{
    public partial class HttpClientInstance
    {
        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request)
        {
            return await SendAsync<TRequest, TResponse>(request, HttpMethod.Post).ConfigureAwait(false);
        }

        public async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request)
        {
            return await SendAsync<TRequest, TResponse>(request, HttpMethod.Get).ConfigureAwait(false);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request)
        {
            return await SendAsync<TRequest, TResponse>(request, HttpMethod.Put).ConfigureAwait(false);
        }

        public async Task<TResponse> DeleteAsync<TRequest, TResponse>(TRequest request)
        {
            return await SendAsync<TRequest, TResponse>(request, HttpMethod.Delete).ConfigureAwait(false);
        }

        public async Task<TResponse> PostAsync<TResponse>()
        {
            return await PostAsync<EmptyRequest, TResponse>(new EmptyRequest()).ConfigureAwait(false);
        }

        public async Task<TResponse> GetAsync<TResponse>()
        {
            return await GetAsync<EmptyRequest, TResponse>(new EmptyRequest()).ConfigureAwait(false);
        }

        public async Task<TResponse> PutAsync<TResponse>()
        {
            return await PutAsync<EmptyRequest, TResponse>(new EmptyRequest()).ConfigureAwait(false);
        }

        public async Task<TResponse> DeleteAsync<TResponse>()
        {
            return await DeleteAsync<EmptyRequest, TResponse>(new EmptyRequest()).ConfigureAwait(false);
        }

        public async Task PostAsync<TRequest>(TRequest request)
        {
            await PostAsync<TRequest, EmptyResponse>(request).ConfigureAwait(false);
        }

        public async Task PutAsync<TRequest>(TRequest request)
        {
            await PutAsync<TRequest, EmptyResponse>(request).ConfigureAwait(false);
        }

        public async Task DeleteAsync<TRequest>(TRequest request)
        {
            await DeleteAsync<TRequest, EmptyResponse>(request).ConfigureAwait(false);
        }
    }
}
