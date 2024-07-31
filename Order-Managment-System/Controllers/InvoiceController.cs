using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Managment_System.Dtos;
using Order_Managment_System.ErrorResponse;

namespace Order_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices invoiceServices;
        private readonly IMapper mapper;

        public InvoiceController(IInvoiceServices invoiceServices , IMapper mapper)
        {
            this.invoiceServices = invoiceServices;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AllInvoices")]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetAllInvoicesAsync()
        {
            var invoices = await invoiceServices.GetAllInvoicesAsync();
            var MappedInvoice = mapper.Map<IReadOnlyList<Invoice>, IReadOnlyList<InvoiceDto>>(invoices);
            if(invoices == null)
            {
                return NotFound(new ApiException(404, "No Invoices Found"));
            }
            return Ok(MappedInvoice);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("invoices/{invoiceId}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByIdAsync(int id)
        {
            var invoice = await invoiceServices.GetInvoiceByIdAsync(id);
            var MappedInvoice = mapper.Map<Invoice , InvoiceDto>(invoice);
            if(invoice == null)
            {
                return NotFound(new ApiException(404, "This Invoice Not Found"));

            }
            return Ok(MappedInvoice);
        }
       
    }
}
