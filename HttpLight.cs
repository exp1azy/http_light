using HttpLight.Exceptions;
using HttpLight.Resources;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HttpLight
{
    /// <summary>
    /// Provides a lightweight HTTP client wrapper for simplified request handling.
    /// </summary>
    public partial class HttpLight : IDisposable
    {
        private readonly HttpClient _httpClient = new();
        private bool _disposed = false;

        /// <summary>
        /// Releases unmanaged resources and disposes the internal <see cref="HttpClient"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Sets default headers for the HTTP client.
        /// </summary>
        /// <param name="headers">The dictionary of headers to set.</param>
        public void SetDefaultHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)           
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);          
        }

        /// <summary>
        /// Removes all default headers from the HTTP client.
        /// </summary>
        public void RemoveDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
        }

        /// <summary>
        /// Sets the timeout for the HTTP client.
        /// </summary>
        /// <param name="timeout">The timeout to set.</param>
        public void SetTimeout(TimeSpan timeout)
        {
            _httpClient.Timeout = timeout;
        }

        /// <summary>
        /// Removes the timeout from the HTTP client.
        /// </summary>
        public void RemoveTimeout()
        {
            _httpClient.Timeout = default;
        }

        /// <summary>
        /// Sets the bearer token for the HTTP client.
        /// </summary>
        /// <param name="token">The bearer token to set.</param>
        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Removes the bearer token from the HTTP client.
        /// </summary>
        public void RemoveAuthorizationHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private async Task<TResult> ProcessWithRetryAsync<TResult>(Func<Task<TResult>> action, int retries, int delay, CancellationToken cancellationToken)
        {
            int retryCount = 0;

            while (true)
            {
                try
                {
                    return await action();
                }
                catch (Exception)
                {
                    retryCount++;
                    if (retryCount >= retries) throw new RetryException(Error.RetryFailed);
                    await Task.Delay(delay, cancellationToken);
                }
            }
        }

        private static async Task<TResult> GetDeserializedAsync<TResult>(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            var stringResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new FailedRequestException(string.Format(Error.RequestFailed, response.StatusCode, stringResponse));

            try
            {
                return typeof(TResult) == typeof(string) ? 
                    (TResult)(object)stringResponse : 
                    JsonConvert.DeserializeObject<TResult>(stringResponse)!;
            }
            catch (Exception ex)
            {
                throw new DeserializeException(string.Format(Error.ConvertFailed, typeof(TResult).Name), ex);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)                
                    _httpClient.Dispose();
                
                _disposed = true;
            }
        }
    }
}
