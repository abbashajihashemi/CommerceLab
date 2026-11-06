namespace Inventories.Api.Controllers.V1.Models;

public class GetVendorProductStockRequest
{
    public IEnumerable<long> ProductCodes { get; set; } = [];
}