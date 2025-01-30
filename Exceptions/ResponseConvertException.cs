namespace HttpLight.Exceptions
{
    public class ResponseConvertException(string message, Exception inner) : Exception(message, inner)
    {
    }
}
