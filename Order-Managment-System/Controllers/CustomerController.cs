using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Managment_System.Dtos;
using Order_Managment_System.ErrorResponse;

namespace Order_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices customerServices;
        private readonly IMapper mapper;

        public CustomerController(ICustomerServices customerServices ,IMapper mapper)
        {
            this.customerServices = customerServices;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> AddNewCustomer(CustomerDto customerDto)
        {
            var customer = mapper.Map<CustomerDto,Customer>(customerDto);
            await customerServices.AddNewCustomer(customer);
            var MappedCustomer = mapper.Map<Customer, CustomerDto>(customer);
            if (customer == null)
            {
              return NotFound(new ApiException(404,"This Customer Not Found"));
            }
            return Ok(MappedCustomer);
        }

        [HttpGet("AllOrdersForTheCustomer")]
        public async Task<IActionResult> GetAllOrdersForTheCustomer()
        {
            var orders = await customerServices.GetAllOrdersForTheCustomer();
            if (orders == null)
            {
                return NotFound(new ApiException(404, "This Order Not Found"));

            }
            return Ok(orders);
        }
    }
}
