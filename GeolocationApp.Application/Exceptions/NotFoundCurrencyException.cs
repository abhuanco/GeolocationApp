using System.Net;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Application.Exceptions
{
    public class NotFoundCurrencyException : BaseException
    {
        public NotFoundCurrencyException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public NotFoundCurrencyException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}