using Core.Models;

namespace Order_Managment_System.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItems> Items { get; set; } = new HashSet<OrderItems>();

        public string Status { get; set; }
        public string PaymentMethod { get; set; }
    }
}