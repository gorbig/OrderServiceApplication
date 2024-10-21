namespace OrderServiceApp.Models.Events;

public class OrderDeleted
{
    public int OrderId { get; set; }
    public DateTime DeletedAt { get; set; }
}