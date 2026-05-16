namespace Resturant.Models;

public class OrderItemsModel
{
    public Guid   Id {get; set;}
    public Guid OrderId { get; set; }
    public Guid DessertItemId { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}