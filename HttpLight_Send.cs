namespace HttpLight
{
    public partial class HttpLight
    {
        /// <summary>
        /// Sends a synchronous HTTP request using the specified <see cref="HttpRequestMessage"/> and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="request">The HTTP request message that contains the details of the request (method, headers, body, etc.).</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult Send<TResult>(HttpRequestMessage request)
        {
            var response = _httpClient.Send(request);
            return GetDeserializedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sends an asynchronous HTTP request using the specified <see cref="HttpRequestMessage"/> and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="request">The HTTP request message that contains the details of the request (method, headers, body, etc.).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> SendAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await GetDeserializedAsync<TResult>(response, cancellationToken);
        }

        /// <summary>
        /// Sends a synchronous HTTP request using the specified <see cref="HttpRequestMessage"/>, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="request">The HTTP request message that contains the details of the request (method, headers, body, etc.).</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult SendWithRetry<TResult>(HttpRequestMessage request, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => SendAsync<TResult>(request), retries, delay, default).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sends an asynchronous HTTP request using the specified <see cref="HttpRequestMessage"/>, retrying the request a specified number of times in case of failure.
        /// The request is retried with a delay between attempts, and the response is deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="request">The HTTP request message that contains the details of the request (method, headers, body, etc.).</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> SendWithRetryAsync<TResult>(HttpRequestMessage request, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => SendAsync<TResult>(request, cancellationToken), retries, delay, cancellationToken);
        }

    }
}
