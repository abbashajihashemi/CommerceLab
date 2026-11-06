using Orders.Application.Orders.Queries;
using Orders.Domain;

namespace Orders.Application.Orders;

[Mapper]
public static partial class OrderMapper
{
    public static partial OrderDto ToOrderDto(this Order order);
    public static partial OrderDetailDto ToOrderDetailDto(this Order order);
}