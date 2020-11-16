using System;
using System.Collections.Generic;
using System.Text;

namespace Subscription.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(int errorCode, string message)
        {
        }
        public ApiException(int errorCode, string message, Exception innerException)
        {
        }

        public ApiException(int errorCode, string message, Type errorContentType, object errorContent = null)
        {
        }

        public int ErrorCode { get; set; }

        public Type ErrorContentType { get; set; }
        public string ErrorContent { get; internal set; }
    }
}
