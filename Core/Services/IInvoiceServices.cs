using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IInvoiceServices
    {
        Task<Invoice> GetInvoiceByIdAsync(int id); //Admin
        Task<IReadOnlyList<Invoice>> GetAllInvoicesAsync(); //Admin

        Task CreateInvoiceAsync(Order order); 
       
    }
}
