using MassTransit;
using OrderServiceApp.Models.Events;

namespace OrderServiceApp.Consumers;

public class OrderDeletedConsumer : IConsumer<OrderDeleted>
{
    public Task Consume(ConsumeContext<OrderDeleted> context)
    {
        var orderDeleted = context.Message;
        Console.WriteLine($"Order deleted: {orderDeleted.OrderId}, Deleted at: {orderDeleted.DeletedAt}");
        return Task.CompletedTask;
    }
}