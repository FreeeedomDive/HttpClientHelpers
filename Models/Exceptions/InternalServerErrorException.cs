using System.Net;

namespace Xdd.HttpHelpers.Models.Exceptions;

public class InternalServerErrorException : HttpResponseExceptionBase
{
    public InternalServerErrorException() : base(string.Empty)
    {
    }

    public InternalServerErrorException(string message) : base(message)
    {
    }

    public InternalServerErrorException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}