using CleanSheet.Domain.Abstractions;

namespace CleanSheet.Domain.Errors;

public class AuthErrors
{
    public static Error InvalidCredentials = new("INVALID_CREDENTIALS", "Invalid credentials. Please review username and password and try again.");
}
