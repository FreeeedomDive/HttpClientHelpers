using System.Net;

namespace Xdd.HttpHelpers.Models.Exceptions;

public class UnauthorizedException : HttpResponseExceptionBase
{
    public UnauthorizedException() : base(string.Empty)
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}