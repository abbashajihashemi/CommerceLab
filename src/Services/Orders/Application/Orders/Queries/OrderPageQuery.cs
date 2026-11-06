using Orders.Domain;

namespace Orders.Application.Orders.Queries;

public sealed class OrderPageQuery(
    OrderPageFilter filter
) : IRequest<Result<PagedResult<OrderDto>>>
{
    public OrderPageFilter Filter { get; init; } = filter;
}

public class OrderPageFilter : PageFilter
{
    public int? VendorCode { get; init; }
    public decimal? MinTotal { get; init; }
    public decimal? MaxTotal { get; init; }
}

public class OrderDto
{
    public Guid Id { get; init; }
    public string Code { get; set; } = null!;
    public int VendorCode { get; set; }
    public string VendorDisplayName { get; set; } = null!;
    public OrderStepStatus StepStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}