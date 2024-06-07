using System.Net;

namespace Xdd.HttpHelpers.Models.Exceptions;

public class NotFoundException : HttpResponseExceptionBase
{
    public NotFoundException() : base(string.Empty)
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception baseException) : base(message, baseException)
    {
    }

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}