using System.Collections.Concurrent;

namespace Inventories.Api.Applications;

public class InMemoryVendorStockService(ILogger<InMemoryVendorStockService> logger) : IVendorStockService
{
    private readonly ConcurrentDictionary<int, ConcurrentDictionary<long, int>> _vendorStock = new()
    {
        [1031] = new()
        {
            [1001] = 100, // Wireless Mouse: plenty
            [1002] = 15, // Mechanical Keyboard: moderate
            [1003] = 3, // USB-C Hub: low, fails when quantity > 3
            [1004] = 0, // 27" Monitor: out of stock
            [1005] = 50, // Laptop Stand
            [1006] = 1, // Webcam HD: only one left
        }
    };

    public Task<IReadOnlyList<VendorProductStockResponse>> GetVendorProductStockAsync(
        int vendorCode,
        IEnumerable<long> productCodes,
        CancellationToken ct = default)
    {
        var ids = productCodes.Distinct().ToArray();
        if (ids.Length == 0)
            return Task.FromResult<IReadOnlyList<VendorProductStockResponse>>([]);

        if (!_vendorStock.TryGetValue(vendorCode, out var stock))
        {
            logger.LogDebug("No stock found for vendor {VendorCode}", vendorCode);
            return Task.FromResult<IReadOnlyList<VendorProductStockResponse>>([]);
        }

        var result = ids
            .Where(stock.ContainsKey)
            .Select(x => new VendorProductStockResponse
            {
                ProductCode = x,
                AvailableQuantity = stock[x]
            })
            .ToList();

        return Task.FromResult<IReadOnlyList<VendorProductStockResponse>>(result);
    }
}

public class VendorProductStockResponse
{
    public long ProductCode { get; set; }
    public int AvailableQuantity { get; set; }
}
