namespace HttpLight
{
    public partial class HttpLight
    {
        public TResult Delete<TResult>(string url)
        {
            var response = _httpClient.DeleteAsync(url).GetAwaiter().GetResult();
            return GetConvertedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        public async Task<TResult> DeleteAsync<TResult>(string url, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(url, cancellationToken);
            return await GetConvertedAsync<TResult>(response, cancellationToken);
        }

        public TResult DeleteWithRetry<TResult>(string url, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => Task.Run(() => Delete<TResult>(url)), retries, delay, default).GetAwaiter().GetResult();
        }

        public async Task<TResult> DeleteWithRetryAsync<TResult>(string url, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => DeleteAsync<TResult>(url, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
