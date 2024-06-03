using System.Net;

namespace Models.Exceptions;

public class InternalServerErrorException : HttpResponseExceptionBase
{
    public InternalServerErrorException(string message) : base(message)
    {
    }

    public InternalServerErrorException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}