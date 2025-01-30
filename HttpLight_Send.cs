namespace HttpLight
{
    public partial class HttpLight
    {
        public TResult Send<TResult>(HttpRequestMessage request)
        {
            var response = _httpClient.Send(request);
            return GetConvertedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        public async Task<TResult> SendAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await GetConvertedAsync<TResult>(response, cancellationToken);
        }

        public TResult SendWithRetry<TResult>(HttpRequestMessage request, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => Task.Run(() => Send<TResult>(request)), retries, delay, default).GetAwaiter().GetResult();
        }

        public async Task<TResult> SendWithRetryAsync<TResult>(HttpRequestMessage request, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => SendAsync<TResult>(request, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
