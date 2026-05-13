using Microsoft.AspNetCore.Mvc;
using VinciOrders.API.DTOs;
using VinciOrders.API.Services;

namespace VinciOrders.API.Controllers;

// Define que esta classe é um Controller de API
// [Route("api/[controller")] significa que a rota base será /orders
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    // O serviço é injetado automaticamente pelo .NET (injeção de dependência)
    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    // GET /orders — retorna todos os pedidos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllAsync();
        return Ok(orders); // HTTP 200 com a lista de pedidos
    }

    // GET /orders/{id} — retorna um pedido específico pelo Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetByIdAsync(id);

        // Se não encontrou, retorna HTTP 404
        if (order == null)
            return NotFound(new { message = "Pedido não encontrado." });

        return Ok(order); // HTTP 200 com o pedido
    }

    // POST /orders — cria um novo pedido
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        try
        {
            var order = await _service.CreateAsync(dto);

            // HTTP 201 Created — indica que um recurso foi criado com sucesso
            // Também informa a URL onde o novo pedido pode ser consultado
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (ArgumentException ex)
        {
            // Se o Service lançou uma exceção de validação, retorna HTTP 400
            return BadRequest(new { message = ex.Message });
        }
    }
}