using CleanGame.Domain.Base;

namespace CleanGame.Domain.Errors;

public static class Global
{
    public static readonly Error InvalidArguments = new("Invalid.Arguments", "invalid arguments");
    public static readonly Error AuthorizationIsEmpty = new("Authorization.IsEmpty", "Authorization token is empty");
    public static readonly Error AuthorizationIsFailed = new("Authorization.IsEmpty", "Invalid authorization token");
    public static readonly Error InternalError = new("InternalError", "Internal error");

}