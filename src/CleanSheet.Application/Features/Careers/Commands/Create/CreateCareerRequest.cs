namespace CleanSheet.Application.Features.Careers.Commands.Create;
public record CreateCareerRequest(
    long UserId,
    string ManagerFirstName,
    string ManagerLastName)
{
    public CreateCareerCommand ToCommand() =>
        new(UserId, ManagerFirstName, ManagerLastName);
};