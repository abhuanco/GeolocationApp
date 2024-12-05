using System.Net;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Application.Exceptions
{
    public class LoadCurrenciesException : BaseException
    {
        public LoadCurrenciesException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public LoadCurrenciesException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }
    }
}