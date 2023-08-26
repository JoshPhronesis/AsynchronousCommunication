using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Dtos;
using Orders.Api.Entities;
using Orders.Api.Services;

namespace Orders.Api.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrdersService _ordersService;
    private readonly ILogger<OrderController> _logger;
    private readonly IMapper _mapper;

    public OrderController(IOrdersService ordersService, 
        ILogger<OrderController> logger, 
        IMapper mapper)
    {
        _ordersService = ordersService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderForReturnDto>>> GetOrders()
    {
        var orders = await _ordersService.GetOrdersAsync();
        
        return Ok(_mapper.Map<IEnumerable<OrderForReturnDto>>(orders));
    }
    [HttpGet("{orderId}", Name = nameof(GetOrder))]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        var orders = await _ordersService.GetOrderByIdAsync(orderId);
        
        return Ok(_mapper.Map<OrderForReturnDto>(orders));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrders([FromBody] OrderForCreateDto orderForCreateDto)
    {
        var order = await _ordersService.CreateOrderAsync(_mapper.Map<Order>(orderForCreateDto));
        var orderDto = _mapper.Map<OrderForReturnDto>(order);
        
        return CreatedAtAction(nameof(GetOrder), new {orderId = order.Id}, new {orderDto});
    }
    
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrders([FromBody] OrderForUpdateDto orderForUpdateDto, Guid orderId)
    {
        var order = _mapper.Map<Order>(orderForUpdateDto);
        order.Id = orderId;
        
        var updateOrder = await _ordersService.UpdateOrderAsync(order);
        var orderDto = _mapper.Map<OrderForReturnDto>(updateOrder);

        return Ok(orderDto);
    }

    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(Guid orderId)
    {
        var result = await _ordersService.DeleteAsync(orderId);
        return result ? NoContent() : BadRequest();
    }
}