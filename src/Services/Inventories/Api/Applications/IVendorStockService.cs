namespace Inventories.Api.Applications;

public interface IVendorStockService
{
    Task<IReadOnlyList<VendorProductStockResponse>> GetVendorProductStockAsync(
        int vendorCode,
        IEnumerable<long> productCodes,
        CancellationToken ct = default);
}
