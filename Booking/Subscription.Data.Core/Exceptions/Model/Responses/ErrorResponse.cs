namespace Subscription.Infrastructure.Exceptions.Model.Responses
{
    public class ErrorResponse : Error
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(int errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
