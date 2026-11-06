namespace Orders.Application.Abstractions.BuildingBlocks.Validation;

public sealed record ValidationError(string Field, string Message);