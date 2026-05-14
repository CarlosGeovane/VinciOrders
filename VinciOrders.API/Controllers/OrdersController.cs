using Microsoft.AspNetCore.Mvc;
using VinciOrders.API.DTOs;
using VinciOrders.API.Services;

namespace VinciOrders.API.Controllers;


[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;


    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllAsync();
        return Ok(orders);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetByIdAsync(id);

        if (order == null)
            return NotFound(new { message = "Pedido não encontrado." });

        return Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var order = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
}