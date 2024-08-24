namespace CleanSheet.Domain.Entities;
public class Manager(
    string firstName,
    string lastName)
{
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
}
