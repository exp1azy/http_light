namespace HttpLight
{
    public partial class HttpLight
    {
        /// <summary>
        /// Sends a synchronous HTTP POST request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="content">The content to be sent with the POST request, typically a JSON string.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult Post<TResult>(string url, StringContent content) =>     
            ProcessPostAsync<TResult>(url, content, default).GetAwaiter().GetResult();
        
        /// <summary>
        /// Sends an asynchronous HTTP POST request to the specified URL with the provided content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="content">The content to be sent with the POST request, typically a JSON string.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PostAsync<TResult>(string url, StringContent content, CancellationToken cancellationToken = default) =>
            await ProcessPostAsync<TResult>(url, content, cancellationToken);

        /// <summary>
        /// Sends a synchronous HTTP POST request with multipart/form-data content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="content">The multipart/form-data content to be included in the request.</param>
        /// <returns>The deserialized response from the server as the <typeparamref name="TResult"/> type.</returns>
        public TResult PostFormData<TResult>(string url, MultipartFormDataContent content) =>
            ProcessPostAsync<TResult>(url, content, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP POST request with multipart/form-data content and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="content">The multipart/form-data content to be included in the request.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation. Default is no cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PostFormDataAsync<TResult>(string url, MultipartFormDataContent content, CancellationToken cancellationToken = default) =>
            await ProcessPostAsync<TResult>(url, content, cancellationToken);

        /// <summary>
        /// Sends a synchronous HTTP POST request to the specified URL with the provided content, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="content">The content to be sent with the POST request, typically a JSON string.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult PostWithRetry<TResult>(string url, StringContent content, int retries = 3, int delay = 5000)  =>
            ProcessWithRetryAsync(() => PostAsync<TResult>(url, content), retries, delay, default).GetAwaiter().GetResult();

        /// <summary>
        /// Sends an asynchronous HTTP POST request to the specified URL with the provided content, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the POST request to.</param>
        /// <param name="content">The content to be sent with the POST request, typically a JSON string.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> PostWithRetryAsync<TResult>(string url, StringContent content, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default) =>
            await ProcessWithRetryAsync(() => PostAsync<TResult>(url, content, cancellationToken), retries, delay, cancellationToken);

        private async Task<TResult> ProcessPostAsync<TResult>(string url, HttpContent content, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsync(url, content, cancellationToken);
            return await GetDeserializedAsync<TResult>(response, cancellationToken);
        }
    }
}
