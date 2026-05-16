namespace Resturant.Models;

public class OrderIsModel
{
    public Guid Id {get; set;}
    public Guid UserId {get; set;}
    public DateTime PickUpDate {get; set;}
    public string Note {get; set;}
    public DateTime CreateAt {get; set;}
    public string Status {get; set;}
}