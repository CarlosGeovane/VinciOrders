using Microsoft.EntityFrameworkCore;
using VinciOrders.API.Data;
using VinciOrders.API.Repositories;
using VinciOrders.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Registra o banco de dados SQLite usando a string de conexão do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Registra o repositório e o serviço para injeção de dependência
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Adiciona os controllers da API
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Customiza as mensagens de erro de validação
        options.InvalidModelStateResponseFactory = context =>
        {
            var erros = context.ModelState
                .Where(e => e.Value!.Errors.Count > 0)
                .SelectMany(e => e.Value!.Errors)
                .Select(e => e.ErrorMessage)
                .Where(msg => !string.IsNullOrWhiteSpace(msg))
                .ToList();

            // Se não tiver mensagem customizada, retorna uma mensagem genérica
            if (!erros.Any())
                erros.Add("Dados inválidos. Verifique os campos enviados.");

            return new BadRequestObjectResult(new { errors = erros });
        };
    });

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