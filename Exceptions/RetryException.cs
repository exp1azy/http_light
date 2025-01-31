namespace HttpLight.Exceptions
{
    /// <summary>
    /// An exception is thrown when the limit of retry attempts to send a request is exhausted.
    /// </summary>
    /// <param name="message"></param>
    public class RetryException(string message) : Exception(message)
    {
    }
}
