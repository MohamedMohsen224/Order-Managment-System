using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Customer: BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Navigation Properties
        public Order Orders { get; set; }
    }
}
