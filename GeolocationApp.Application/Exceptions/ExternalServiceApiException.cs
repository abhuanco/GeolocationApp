using System.Net;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Application.Exceptions
{
    public class ExternalServiceApiException: BaseException
    {
        public ExternalServiceApiException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public ExternalServiceApiException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}