using webApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace webApplication.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
    }
}
