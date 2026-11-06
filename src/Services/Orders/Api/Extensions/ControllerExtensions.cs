using Microsoft.AspNetCore.Mvc;
using Orders.Application.Abstractions.BuildingBlocks.Results;

namespace Orders.Api.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToProblem(this ControllerBase controller, Error error)
        => controller.Problem(
            title: error.Code,
            detail: error.Message,
            statusCode: error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status400BadRequest
            });
}