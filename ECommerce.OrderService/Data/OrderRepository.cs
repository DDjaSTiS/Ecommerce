using ECommerce.OrderService.Models;
using Npgsql;

namespace ECommerce.OrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration) 
        {
            _connectionString = configuration["ConnectionString"]!;
        }

        public async Task<Order?> Get(int id, CancellationToken token)
        {
            var query = @"select 
                            * 
                        from
                            public.""Order"" o
                        where
                            o.""Id"" = :id";

            await using var connection = new NpgsqlConnection(_connectionString);
            await using var command = new NpgsqlCommand(query, connection)
            {
                Parameters = { "id", id }
            };

            await connection.OpenAsync(token);
            await using var reader = await command.ExecuteReaderAsync(token);
            Order? order = null;
            while (await reader.ReadAsync())
            {
                order = new Order();
                order.Id = reader.GetFieldValue<int>(0);
                order.CustomerId = reader.GetFieldValue<int>(1);
                order.Region = reader.GetFieldValue<string>(2);
            }

            return order;            
        }

        public IAsyncEnumerable<Order> GetAll(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
