namespace LaNacion.Common.Enum
{
    public enum  KnownErrorCodesEnum
    {
        UnknownError = 0,

        InvalidPropertyLength = 10001,

        InvalidEmail = 20001,

        UserNotFound = 40404,
        UserInactive = 40002,
        UserBlocked = 40003,
        UserAlreadyExists = 40004,

        InvalidPayload = 50001,
        InvalidSecurityAnswers = 50001,
        TokenExpired = 50401,
        TokenInvalid = 50402,
        TokenNotFound = 50404,

        CompanyNotFound = 600404,
    }
}
