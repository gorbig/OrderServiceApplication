namespace OrderServiceApp.Models.Events;

public class OrderUpdated
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime UpdatedAt { get; set; }
}