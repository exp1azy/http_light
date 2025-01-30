namespace HttpLight
{
    public partial class HttpLight
    {
        public TResult Put<TResult>(string url, StringContent content)
        {
            var response = _httpClient.PutAsync(url, content).GetAwaiter().GetResult();
            return GetConvertedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        public async Task<TResult> PutAsync<TResult>(string url, StringContent content, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsync(url, content, cancellationToken);
            return await GetConvertedAsync<TResult>(response, cancellationToken);
        }

        public TResult PutWithRetry<TResult>(string url, StringContent content, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => Task.Run(() => Put<TResult>(url, content)), retries, delay, default).GetAwaiter().GetResult();
        }

        public async Task<TResult> PutWithRetryAsync<TResult>(string url, StringContent content, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => PutAsync<TResult>(url, content, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
