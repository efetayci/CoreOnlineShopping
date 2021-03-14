using CoreOnlineShopping.Data;
using CoreOnlineShopping.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Repository
{
    public class ProductsRepository:GenericRepository<Products>
    {
        ApplicationDbContext _c = new ApplicationDbContext();
        public List<Products> PList()
        {
            return _c.Products.Include(z => z.ProductTypes).Include(z => z.SpecialTag).ToList();
        }
    }
}
