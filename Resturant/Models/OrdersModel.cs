namespace Resturant.Models;

public class OrdersModel
{
    public Guid Id {get; set;}
    public Guid UserId {get; set;}
    public DateTime PickUpDate {get; set;}
    public string? Note {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public string Status {get; set;}
}