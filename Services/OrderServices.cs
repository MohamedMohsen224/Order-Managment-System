using Core;
using Core.Models;
using Core.Models.Enum;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderServices: IOrderServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IInvoiceServices invoiceServices;

        public OrderServices(IUnitOfWork unitOfWork , IInvoiceServices invoiceServices)
        {
            this.unitOfWork = unitOfWork;
            this.invoiceServices = invoiceServices;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var isStockAvailable = await ensureproductstock(order);

             if (isStockAvailable)
             {
                //calculate total amount
                 order.TotalAmount = CalculateTotalAmount(order);
                //apply discount
                ApplayDiscount(order);
                //update product stock
                await UpdateProductStock(order);
                //create order
                var newOrder = new Order( order.CustomerId,order.OrderDate,order.TotalAmount,order.PaymentMethod,order.Status);
                //create order invoice
                 await invoiceServices.CreateInvoiceAsync(newOrder);

                return newOrder;


                
             }
             else
             {
                 return null;
             }
            
            
        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync()
        {
           var orders = await unitOfWork.Repository<Order>().GetAllSpecAsync();
            return orders;
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            var order = unitOfWork.Repository<Order>().GetByIdSpecAsync(id);
            return order;
        }

        public async Task updateOrder(int orderId ,Status status)
        {
            var order = await unitOfWork.Repository<Order>().GetByIdSpecAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.Status = status;
            unitOfWork.Repository<Order>().UpdateAsync(order);
            await unitOfWork.CompleteAsync();
        }





        // PrivateFunctions

        private async Task<bool> ensureproductstock(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await unitOfWork.Repository<Product>().GetByIdSpecAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    return false;
                }
            }
            return true;

        }

        private void ApplayDiscount(Order order)
        {
            if(order.TotalAmount > 100)
            {
                order.TotalAmount *= .5M;
            }
            else if(order.TotalAmount > 200)
            {
                order.TotalAmount *= .10M;
            }
        }

        private decimal CalculateTotalAmount(Order order)
        {
            decimal total = 0;
            foreach (var item in order.OrderItems)
            {
                var product = unitOfWork.Repository<Product>().GetByIdSpecAsync(item.ProductId).Result;
                total += product.Price * item.Quantity;
            }
            return total;
        }

        private async Task UpdateProductStock(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await unitOfWork.Repository<Product>().GetByIdSpecAsync(item.ProductId);
                product.Stock -= item.Quantity;
                unitOfWork.Repository<Product>().UpdateAsync(product);
                unitOfWork.CompleteAsync();
            }
        }
    }
}
