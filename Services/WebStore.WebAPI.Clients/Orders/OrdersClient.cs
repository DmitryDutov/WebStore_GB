using System.Net.Http.Json;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Orders;

public class OrdersClient: BaseClient, IOrderService
{
    public OrdersClient(HttpClient Client):base(Client, "api/orders")
    {
        
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string UserName, CancellationToken Cancel = default)
    {
        var orders = await GetAsync<IEnumerable<OrderDTO>>($"{Addres}/user/{UserName}").ConfigureAwait(false);
        return orders!.FromDTO()!;
    }

    public async Task<Order?> GetOrderByIdAsync(int Id, CancellationToken Cancel = default)
    {
        var order = await GetAsync<OrderDTO>($"{Addres}/{Id}").ConfigureAwait(false);
        return order.FromDTO();
    }

    public async Task<Order> CreateOrderAsync(string UserName, CartViewModel Cart, OrderViewModel OrderModel,
        CancellationToken Cancel = default)
    {
        var model = new CreateOrderDTO()
        {
            Items = Cart.ToDTO(),
            Order = OrderModel,
        };

        var responce = await PostAsync($"{Addres}/{UserName}", model).ConfigureAwait(false);
        var oreder=await responce
            .EnsureSuccessStatusCode()
            .Content
            .ReadFromJsonAsync<OrderDTO>(cancellationToken:Cancel)
            .ConfigureAwait(false);

        return oreder.FromDTO()!;
    }
}