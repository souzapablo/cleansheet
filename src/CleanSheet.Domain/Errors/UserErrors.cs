using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;
public class UserErrors
{
    public static Error EmailAlreadyRegistered = new("EMAIL_ALREADY_REGISTERED", "E-mail is already registered.");

    public static Error NotFound = new("USER_NOT_FOUND", "User not found.");
}
