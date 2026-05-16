namespace Resturant.Dtos;

public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public DateTime PickUpDate { get; set; }
    public string? Note { get; set; }
}