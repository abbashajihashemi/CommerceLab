using Orders.Application.Orders.Queries;
using Orders.Domain;

namespace Orders.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task<PagedResult<Order>> SearchAsync(OrderPageFilter filter, CancellationToken ct = default);
    Task<Order?> GetOrderWithItemsByIdAsync(Guid id, CancellationToken ct = default);
}