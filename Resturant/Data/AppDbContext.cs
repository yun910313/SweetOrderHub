using Microsoft.EntityFrameworkCore;
using Resturant.Models;

namespace Resturant.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<DessertItemModel> DessertItems { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }
    }
}
