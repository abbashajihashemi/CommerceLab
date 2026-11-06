using System.ComponentModel.DataAnnotations;

namespace Orders.Infrastructure.Clients.Inventory;

public class InventoryClientOptions
{
    public const string Section = "Services:Inventory";

    [Required, Url]
    public string BaseUrl { get; init; } = null!;
}