namespace HttpLight.Exceptions
{
    /// <summary>
    /// An exception is thrown when an error occurs while sending a request.
    /// </summary>
    /// <param name="message"></param>
    public class FailedRequestException(string message) : Exception(message)
    {
    }
}
