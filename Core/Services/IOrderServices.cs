using Core.Models;
using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IOrderServices
    {

        Task<Order> CreateOrderAsync(Order order);//User
        Task<Order> GetOrderByIdAsync(int id); //User
        Task<IReadOnlyList<Order>> GetAllOrdersAsync(); //Admin
        Task updateOrder(int orderId , Status status); //Admin

    }
}
