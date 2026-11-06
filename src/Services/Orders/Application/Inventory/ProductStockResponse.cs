namespace Orders.Application.Inventory;

public class ProductStockResponse
{
    public long ProductCode { get; set; }
    public int AvailableQuantity { get; set; }

    public bool Available(int quantity) => AvailableQuantity >= quantity;
}