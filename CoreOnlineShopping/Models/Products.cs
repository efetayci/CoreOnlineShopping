using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string ProductColor { get; set; }
        public bool IsAvilable { get; set; }
        public int ProductTypesId { get; set; }

        public virtual ProductTypes ProductTypes { get; set; }
        public int SpecialTagId { get; set; }

        public virtual SpecialTag SpecialTag { get; set; }
    }
}
