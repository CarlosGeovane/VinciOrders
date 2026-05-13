using Microsoft.EntityFrameworkCore;
using VinciOrders.API.Models;

namespace VinciOrders.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Order> Orders { get; set; }
}