using System.Net;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Application.Exceptions;

public class DataDeserializeException: BaseException
{
    public DataDeserializeException(HttpStatusCode statusCode) : base(statusCode)
    {
    }

    public DataDeserializeException(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
}