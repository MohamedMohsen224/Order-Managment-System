using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Invoice: BaseModel
    {
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public Invoice()
        {
            
        }
        public Invoice(int orderId, decimal totalAmount, Order order)
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
            Order = order;
        }
    }
}
