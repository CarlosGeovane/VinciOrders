using Microsoft.EntityFrameworkCore;
using VinciOrders.API.Models;

namespace VinciOrders.API.Data;

// Gerenciador do banco de dados da aplicação
// Herda de DbContext, que é a classe base do Entity Framework
public class AppDbContext : DbContext
{
    // Construtor que recebe as configurações do banco (vem do Program.cs)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Representa a tabela "Orders" no banco de dados
    // O EF Core usa essa propriedade para saber que a tabela existe
    public DbSet<Order> Orders { get; set; }
}