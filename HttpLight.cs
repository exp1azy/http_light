using HttpLight.Exceptions;
using HttpLight.Resources;
using Newtonsoft.Json;

namespace HttpLight
{
    public partial class HttpLight : IDisposable
    {
        private readonly HttpClient _httpClient = new();
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        private static async Task<TResult> GetConvertedAsync<TResult>(HttpResponseMessage response, CancellationToken cancellationToken = default)
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
                throw new ResponseConvertException(string.Format(Error.ConvertFailed, typeof(TResult).Name), ex);
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
