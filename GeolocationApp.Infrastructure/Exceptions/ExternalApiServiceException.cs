using System.Net;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Infrastructure.Exceptions
{
    public class ExternalApiServiceException : BaseException
    {
        public ExternalApiServiceException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public ExternalApiServiceException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}