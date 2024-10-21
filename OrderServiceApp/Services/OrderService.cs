using MassTransit;
using OrderServiceApp.IRepositories;
using OrderServiceApp.IServices;
using OrderServiceApp.Models;
using OrderServiceApp.Models.Events;

namespace OrderServiceApp.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint; 
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task AddOrderAsync(Order order)
    {
        await _orderRepository.AddAsync(order);

        _logger.LogInformation("Публикация события OrderCreated для заказа {OrderId}", order.Id);

        try
        {
            // Использую _publishEndpoint для публикации события
            await _publishEndpoint.Publish(new OrderCreated
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount,
                CreatedAt = DateTime.UtcNow
            });

            _logger.LogInformation("Событие OrderCreated успешно опубликовано для заказа {OrderId}", order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при публикации события OrderCreated для заказа {OrderId}", order.Id);
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await _orderRepository.UpdateAsync(order);

        _logger.LogInformation("Публикация события OrderUpdated для заказа {OrderId}", order.Id);

        try
        {
            await _publishEndpoint.Publish(new OrderUpdated
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount,
                UpdatedAt = DateTime.UtcNow
            });

            _logger.LogInformation("Событие OrderUpdated успешно опубликовано для заказа {OrderId}", order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при публикации события OrderUpdated для заказа {OrderId}", order.Id);
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);

        _logger.LogInformation("Публикация события OrderDeleted для заказа {OrderId}", id);

        try
        {
            await _publishEndpoint.Publish(new OrderDeleted
            {
                OrderId = id,
                DeletedAt = DateTime.UtcNow
            });

            _logger.LogInformation("Событие OrderDeleted успешно опубликовано для заказа {OrderId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при публикации события OrderDeleted для заказа {OrderId}", id);
        }
    }

    public async Task<bool> OrderExistsAsync(int id)
    {
        return await _orderRepository.ExistsAsync(id);
    }
}
