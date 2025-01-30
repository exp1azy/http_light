namespace HttpLight
{
    public partial class HttpLight
    {
        public TResult Post<TResult>(string url, StringContent content)
        {
            var response = _httpClient.PostAsync(url, content).GetAwaiter().GetResult();
            return GetConvertedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        public async Task<TResult> PostAsync<TResult>(string url, StringContent content, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(url, content, cancellationToken);
            return await GetConvertedAsync<TResult>(response, cancellationToken);
        }

        public TResult PostWithRetry<TResult>(string url, StringContent content, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => Task.Run(() => Post<TResult>(url, content)), retries, delay, default).GetAwaiter().GetResult();
        }

        public async Task<TResult> PostWithRetryAsync<TResult>(string url, StringContent content, int retries = 3, int delay = 5000,  CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => PostAsync<TResult>(url, content, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
