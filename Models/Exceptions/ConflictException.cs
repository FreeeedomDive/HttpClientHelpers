using System.Net;

namespace Xdd.HttpHelpers.Models.Exceptions;

public class ConflictException : HttpResponseExceptionBase
{
    public ConflictException() : base(string.Empty)
    {
    }

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}