namespace HttpLight
{
    public partial class HttpLight
    {
        /// <summary>
        /// Sends a synchronous HTTP DELETE request to the specified URL and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the DELETE request to.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult Delete<TResult>(string url)
        {
            var response = _httpClient.DeleteAsync(url).GetAwaiter().GetResult();
            return GetDeserializedAsync<TResult>(response).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sends an asynchronous HTTP DELETE request to the specified URL and returns the response deserialized to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the DELETE request to.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> DeleteAsync<TResult>(string url, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(url, cancellationToken);
            return await GetDeserializedAsync<TResult>(response, cancellationToken);
        }

        /// <summary>
        /// Sends a synchronous HTTP DELETE request to the specified URL with retry logic, and returns the response deserialized to the specified type.
        /// The request is retried a specified number of times with a delay between each attempt.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the DELETE request to.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <returns>The deserialized response of type <typeparamref name="TResult"/>.</returns>
        public TResult DeleteWithRetry<TResult>(string url, int retries = 3, int delay = 5000)
        {
            return ProcessWithRetryAsync(() => DeleteAsync<TResult>(url), retries, delay, default).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sends an asynchronous HTTP DELETE request to the specified URL with retry logic, and returns the response deserialized to the specified type.
        /// The request is retried a specified number of times with a delay between each attempt.
        /// </summary>
        /// <typeparam name="TResult">The type to which the response will be deserialized.</typeparam>
        /// <param name="url">The URL to send the DELETE request to.</param>
        /// <param name="retries">The number of times to retry the request in case of failure (default is 3).</param>
        /// <param name="delay">The delay in milliseconds between retries (default is 5000).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the deserialized response of type <typeparamref name="TResult"/>.</returns>
        public async Task<TResult> DeleteWithRetryAsync<TResult>(string url, int retries = 3, int delay = 5000, CancellationToken cancellationToken = default)
        {
            return await ProcessWithRetryAsync(() => DeleteAsync<TResult>(url, cancellationToken), retries, delay, cancellationToken);
        }
    }
}
