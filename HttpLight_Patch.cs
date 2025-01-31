namespace HttpLight
{
    public partial class HttpLight
    {
        /// <summary>
        /// Sends a synchronous HTTP PATCH request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PATCH request to.</param>
        /// <param name="content">The content to be sent with the PATCH request, typically a JSON string.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult Patch<TResult>(string url, StringContent content) =>
            ProcessPatchAsync<TResult>(url, content, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP PATCH request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PATCH request to.</param>
        /// <param name="content">The content to be sent with the PATCH request, typically a JSON string.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PatchAsync<TResult>(string url, StringContent content, CancellationToken cancellationToken = default) =>
            await ProcessPatchAsync<TResult>(url, content, cancellationToken);

        /// <summary>
        /// Sends a synchronous HTTP PATCH request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PATCH request to.</param>
        /// <param name="content">The content to be sent with the PATCH request, typically a JSON string.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult PatchFormData<TResult>(string url, MultipartFormDataContent content) => 
            ProcessPatchAsync<TResult>(url, content, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP PATCH request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PATCH request to.</param>
        /// <param name="content">The content to be sent with the PATCH request, typically a JSON string.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PatchFormDataAsync<TResult>(string url, MultipartFormDataContent content, CancellationToken cancellationToken = default) => 
            await ProcessPatchAsync<TResult>(url, content, cancellationToken);

        /// <summary>
        /// Sends a synchronous HTTP PATCH request to the specified URL with the provided content, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PATCH request to.</param>
        /// <param name="content">The content to be sent with the PATCH request, typically a JSON string.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult PatchWithRetry<TResult>(string url, StringContent content, int retries = 3, int delay = 5000) =>
            ProcessWithRetryAsync(() => PatchAsync<TResult>(url, content), retries, delay, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP PATCH request to the specified URL with the provided content, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PATCH request to.</param>
        /// <param name="content">The content to be sent with the PATCH request, typically a JSON string.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PatchWithRetryAsync<TResult>(string url, StringContent content, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default) =>
            await ProcessWithRetryAsync(() => PatchAsync<TResult>(url, content, cancellationToken), retries, delay, cancellationToken);

        private async Task<TResult> ProcessPatchAsync<TResult>(string url, HttpContent content, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PatchAsync(url, content, cancellationToken);
            return await GetDeserializedAsync<TResult>(response, cancellationToken);
        }
    }
}
