using Asp.Versioning;
using Inventories.Api.Applications;
using Inventories.Api.Controllers.V1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventories.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.Prefix + "/vendors/{vendorCode:int:min(1)}/stock")]
public class VendorStockController(IVendorStockService vendorStockService) : ApiControllerBase
{
    [HttpPost("products")]
    public async Task<IActionResult> GetVendorProductStock(
        [FromRoute] int vendorCode,
        [FromBody] GetVendorProductStockRequest request,
        CancellationToken ct = default)
    {
        var result = await vendorStockService.GetVendorProductStockAsync(vendorCode, request.ProductCodes, ct);

        return Ok(new { data = result });
    }
}
