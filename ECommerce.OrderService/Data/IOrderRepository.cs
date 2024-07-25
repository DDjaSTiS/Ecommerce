using ECommerce.OrderService.Models;

namespace ECommerce.OrderService.Data
{
    public interface IOrderRepository
    {
        Task<Order?> Get(int id, CancellationToken token);
        IAsyncEnumerable<Order> GetAll(CancellationToken token);
    }
}
