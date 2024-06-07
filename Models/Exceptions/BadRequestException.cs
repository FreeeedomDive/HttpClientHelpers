using System.Net;

namespace Xdd.HttpHelpers.Models.Exceptions;

public class BadRequestException : HttpResponseExceptionBase
{
    public BadRequestException() : base(string.Empty)
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}