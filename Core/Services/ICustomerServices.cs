using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICustomerServices
    {
        Task AddNewCustomer (Customer customer);

        Task<IReadOnlyList<Order>> GetAllOrdersForTheCustomer ();
    }
}
