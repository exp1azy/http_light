namespace HttpLight
{
    public partial class HttpLight
    {
        /// <summary>
        /// Sends a synchronous HTTP PUT request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="content">The content to be sent with the PUT request, typically a JSON string.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult Put<TResult>(string url, StringContent content) =>
            ProcessPutAsync<TResult>(url, content, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP PUT request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="content">The content to be sent with the PUT request, typically a JSON string.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PutAsync<TResult>(string url, StringContent content, CancellationToken cancellationToken = default) =>
            await ProcessPutAsync<TResult>(url, content, cancellationToken);

        /// <summary>
        /// Sends a synchronous HTTP PUT request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="content">The content to be sent with the PUT request, typically a JSON string.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult PutFormData<TResult>(string url, MultipartFormDataContent content) => 
            ProcessPutAsync<TResult>(url, content, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP PUT request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="content">The content to be sent with the PUT request, typically a JSON string.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PutFormDataAsync<TResult>(string url, MultipartFormDataContent content, CancellationToken cancellationToken = default) => 
            await ProcessPutAsync<TResult>(url, content, cancellationToken);

        /// <summary>
        /// Sends a synchronous HTTP PUT request to the specified URL with the provided content, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="content">The content to be sent with the PUT request, typically a JSON string.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult PutWithRetry<TResult>(string url, StringContent content, int retries = 3, int delay = 5000) =>
            ProcessWithRetryAsync(() => PutAsync<TResult>(url, content), retries, delay, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP PUT request to the specified URL with the provided content, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the PUT request to.</param>
        /// <param name="content">The content to be sent with the PUT request, typically a JSON string.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PutWithRetryAsync<TResult>(string url, StringContent content, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default) =>
            await ProcessWithRetryAsync(() => PutAsync<TResult>(url, content, cancellationToken), retries, delay, cancellationToken);

        private async Task<TResult> ProcessPutAsync<TResult>(string url, HttpContent content, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PutAsync(url, content, cancellationToken);
            return await GetDeserializedAsync<TResult>(response, cancellationToken);
        }
    }
}