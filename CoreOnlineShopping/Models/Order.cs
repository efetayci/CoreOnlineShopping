using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public String OrderNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
