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
    public class CustomerServices : ICustomerServices
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task AddNewCustomer(Customer customer)
        {
            await unitOfWork.Repository<Customer>().AddAsync(customer);
           await unitOfWork.CompleteAsync();
            
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersForTheCustomer()
        {
          return await unitOfWork.Repository<Order>().GetAllSpecAsync();
            
        }
    }
}
