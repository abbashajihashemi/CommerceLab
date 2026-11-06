namespace Orders.Application.Inventories;

public interface IInventoryClient
{
    Task<IReadOnlyList<ProductStockResponse>> GetStockAsync(int vendorCode, IEnumerable<long> productCodes, CancellationToken ct = default);
}