using Core.Models.Enum;

namespace Core.Models
{
    public class Order: BaseModel
    {
        //constructor

        public Order()
        {
            
        }
        public Order(int customerId, DateTime orderDate, decimal totalAmount, string paymentMethod, Status status)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            Status = status;
        }
        //properties
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public Status Status { get; set; }


        // Navigation Properties
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }

    }
}