namespace HttpLight.Exceptions
{
    /// <summary>
    /// Exception thrown when the response could not be deserialized into a custom type.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    public class DeserializeException(string message, Exception inner) : Exception(message, inner)
    {
    }
}
