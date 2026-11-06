using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Orders.Application.Inventories;

namespace Orders.Infrastructure.Clients.Inventories;

public class InventoryClient(
    HttpClient httpClient,
    ILogger<InventoryClient> logger
) : IInventoryClient
{
    public async Task<IReadOnlyList<ProductStockResponse>> GetStockAsync(
        int vendorCode,
        IEnumerable<long> productCodes,
        CancellationToken ct = default)
    {
        var ids = productCodes.Distinct().ToArray();
        if (ids.Length == 0)
        {
            logger.LogWarning("Stock inquiry with invalid empty productCodes");
            return [];
        }

        var body = new StockQueryRequest(ids);
        logger.LogInformation("Stock inquiry sending request {body}", body);

        var route = $"api/v1/vendors/{vendorCode}/stock";
        using var response = await httpClient.PostAsJsonAsync(route, body, ct);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ProductStockResponse>>(ct) ?? [];
    }

    private sealed record StockQueryRequest(IReadOnlyCollection<long> ProductCodes);
}