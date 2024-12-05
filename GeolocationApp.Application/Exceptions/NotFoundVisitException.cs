using System.Net;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Application.Exceptions
{
    public class NotFoundVisitException : BaseException
    {
        public NotFoundVisitException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public NotFoundVisitException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}