using System.Net;

namespace Models.Exceptions;

public class ConflictException : HttpResponseExceptionBase
{
    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}