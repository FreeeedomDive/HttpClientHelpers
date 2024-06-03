using System.Net;

namespace Models.Exceptions;

public abstract class HttpResponseExceptionBase : Exception
{
    protected HttpResponseExceptionBase(string message) : base(message)
    {
    }

    protected HttpResponseExceptionBase(string message, Exception baseException) : base(message, baseException)
    {
    }

    public abstract HttpStatusCode StatusCode { get; }
}