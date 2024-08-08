namespace CleanSheet.Domain.Abstractions;
public interface IValidationResult
{
    public static readonly Error ValidationError = new("VALIDATION_ERROR", "One ore more validation errors occurred.");
    Error[] Errors { get; }
}
