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
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAllAsync();
        return Ok(orders);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetByIdAsync(id);


        if (order == null)
            return NotFound(new { message = "Pedido não encontrado." });

        return Ok(order);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        try
        {
            var order = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}