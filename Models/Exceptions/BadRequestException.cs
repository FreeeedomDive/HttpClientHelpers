using System.Net;

namespace Models.Exceptions;

public class BadRequestException : HttpResponseExceptionBase
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}