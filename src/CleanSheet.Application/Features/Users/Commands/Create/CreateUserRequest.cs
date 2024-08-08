namespace CleanSheet.Application.Features.Users.Commands.Create;
public record CreateUserRequest(
    string Username,
    string Password)
{
    public CreateUserCommand ToCommand() => 
        new(Username, Password);
};