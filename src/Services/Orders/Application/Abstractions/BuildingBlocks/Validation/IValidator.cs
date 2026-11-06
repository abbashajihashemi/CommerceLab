namespace Orders.Application.Abstractions.BuildingBlocks.Validation;

public interface IValidator<in TRequest>
{
    IEnumerable<ValidationError> Validate(TRequest request);
}