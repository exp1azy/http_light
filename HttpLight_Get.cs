namespace HttpLight
{
    public partial class HttpLight
    {
        public TResult Get<TResult>(string url)
        {
            var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();
            return GetConvertedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        public async Task<TResult> GetAsync<TResult>(string url, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            return await GetConvertedAsync<TResult>(response, cancellationToken);
        }

        public TResult GetWithRetry<TResult>(string url, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => Task.Run(() => Get<TResult>(url)), retries, delay, default).GetAwaiter().GetResult();
        }

        public async Task<TResult> GetWithRetryAsync<TResult>(string url, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => GetAsync<TResult>(url, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
