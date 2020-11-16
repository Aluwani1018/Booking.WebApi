namespace Subscription.Infrastructure.Exceptions.Model.Enum
{
    public enum ErrorEnum
    {
        GeneralError,
        UserAlreadyExist,
        InvalidUser,
        UserNotSubscribed,
        InvalidCredentials,
        InvalidRefreshToken,
        InvalidToken,
        InvalidExpiration,
        ExpiredRefreshToken,
        BookAlreadyExist,
    }
}
