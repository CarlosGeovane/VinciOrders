using Microsoft.EntityFrameworkCore;
using VinciOrders.API.Data;
using VinciOrders.API.Repositories;
using VinciOrders.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra o banco de dados SQLite usando a string de conexão do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Registra o repositório e o serviço para injeção de dependência
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Adiciona os controllers da API
builder.Services.AddControllers();

// Adiciona o Swagger para visualizar e testar a API no navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplica as migrations pendentes automaticamente ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync(); 
}

// Habilita o Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();