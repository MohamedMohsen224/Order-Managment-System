namespace Order_Managment_System.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }=DateTime.Now;
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
    }
}