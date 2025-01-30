namespace HttpLight
{
    public partial class HttpLight
    {
        public TResult Patch<TResult>(string url, StringContent content)
        {
            var response = _httpClient.PatchAsync(url, content).GetAwaiter().GetResult();
            return GetConvertedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        public async Task<TResult> PatchAsync<TResult>(string url, StringContent content, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsync(url, content, cancellationToken);
            return await GetConvertedAsync<TResult>(response, cancellationToken);
        }

        public TResult PatchWithRetry<TResult>(string url, StringContent content, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => Task.Run(() => Patch<TResult>(url, content)), retries, delay, default).GetAwaiter().GetResult();
        }

        public async Task<TResult> PatchWithRetryAsync<TResult>(string url, StringContent content, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => PatchAsync<TResult>(url, content, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
