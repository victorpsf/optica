namespace Shared.Exceptions;

public class NotAuthoriazedException: Exception
{
    public NotAuthoriazedException(string? message, Exception? innerException) : base(message, innerException) { }
    public NotAuthoriazedException(string? message) : this(message, null) { }
    public NotAuthoriazedException() : this(null) { }
    
}