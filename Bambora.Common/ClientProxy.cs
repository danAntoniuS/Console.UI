using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Bambora.Common
{
    public class ClientProxy:IClientProxy
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// HttpClient configured as Singleton and injected with DI
        /// </summary>
        /// <param name="httpClient"></param>
        public ClientProxy(HttpClient httpClient)
        {
            _httpClient = Validator.ThrowIfNull(httpClient, nameof(httpClient));
        }
        
        public async Task<T> GetAsync<T>(string url)
        {
            using (var response = await _httpClient.GetAsync(url)
                                            .ConfigureAwait(continueOnCapturedContext: false)) { 
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        T result = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
                        return result;
                    default:
                        HandleErrorStatus(response.StatusCode, $"{_httpClient.BaseAddress}{url}");
                        return default(T);
                }
            };
        }

        private void HandleErrorStatus(HttpStatusCode statusCode, string url)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new Exception($"AlphaVantage Service resource was not found at url: {url}");
                case HttpStatusCode.BadRequest:
                    throw new Exception($"AlphaVantage Service returned a bad request status: {url}");
                case HttpStatusCode.InternalServerError:
                    throw new Exception($"AlphaVantage returned internal server error status: {url}");
                case HttpStatusCode.Unauthorized:
                    throw new Exception($"AlphaVantage returned an unauthorized status: {url}");
                case HttpStatusCode.RequestTimeout:
                    throw new Exception($"AlphaVantage returned a request timeout status: {url}");
                default:
                    throw new Exception($"AlphaVantage returned an unknown status code: {url}");
            }
        }
    }
}
