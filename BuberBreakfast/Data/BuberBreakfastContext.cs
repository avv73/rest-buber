using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Data
{
    public class BuberBreakfastContext(DbContextOptions<BuberBreakfastContext> context) : DbContext(context)
    {
        public DbSet<Breakfast> Breakfasts { get; set; } = null!;
    }
}
