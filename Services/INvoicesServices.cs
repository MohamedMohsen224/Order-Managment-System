using Core;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class INvoicesServices : IInvoiceServices
    {
        private readonly IUnitOfWork unitOfWork;

        public INvoicesServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Invoice>> GetAllInvoicesAsync()
        {
            return await unitOfWork.Repository<Invoice>().GetAllSpecAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await unitOfWork.Repository<Invoice>().GetByIdSpecAsync(id);
        }

        public async Task CreateInvoiceAsync(Order order)
        {
            var invoice = new Invoice(order.Id, order.TotalAmount, order);

            await unitOfWork.Repository<Invoice>().AddAsync(invoice);
            await unitOfWork.CompleteAsync();
        }
    }
}
