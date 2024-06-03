using System.Net;

namespace Models.Exceptions;

public class UnauthorizedException : HttpResponseExceptionBase
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}