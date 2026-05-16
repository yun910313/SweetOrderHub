using System.IdentityModel.Tokens.Jwt;

namespace Resturant.Models
{
    public class CartItemModel
    {
        public Guid Id { get; set; }
        public Guid DessertItemId { get; set; }
        public DessertItemModel DessertItem { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
