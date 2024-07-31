using System.ComponentModel.DataAnnotations;

namespace Order_Managment_System.Dtos
{
    public class CustomerDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
