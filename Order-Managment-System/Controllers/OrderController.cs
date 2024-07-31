using AutoMapper;
using Core.Models;
using Core.Models.Enum;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Managment_System.Dtos;
using Order_Managment_System.ErrorResponse;
using Services;

namespace Order_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices orderServices;
        private readonly IMapper mapper;

        public OrderController(IOrderServices orderServices,IMapper mapper)
        {
            this.orderServices = orderServices;
            this.mapper = mapper;
        }


        [HttpPost("products")]
        public async Task<ActionResult<OrderDto>> CreateOrderAsync(OrderDto orderDto)
        {
            var order = mapper.Map<OrderDto, Order>(orderDto);
            await orderServices.CreateOrderAsync(order);
            var ordersDto = mapper.Map<Order, OrderDto>(order);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(ordersDto);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("AllOrders")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrdersAsync()
        {
           
            var orders = await orderServices.GetAllOrdersAsync();
            var ordersDto = mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderDto>>(orders);
            if (orders == null)
            {
                return NotFound();

            }
            return Ok(ordersDto);
        }



        [HttpGet("orders /{orderId}")]
        public async Task<ActionResult<Order>> GetOrderByIdAsync(int id)
        {
            var order = await orderServices.GetOrderByIdAsync(id);
            var orderDto = mapper.Map<Order, OrderDto>(order);
            if (order == null)
            {
                return NotFound(new ApiException(404, "This Order Not Found"));
            }
            return Ok(orderDto);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, Status status)
        {
             orderServices.updateOrder(orderId, status);
            return Ok("Status Changed");
        }

    }
}
