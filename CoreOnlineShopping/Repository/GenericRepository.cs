
using CoreOnlineShopping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOnlineShopping.Repository
{
    public class GenericRepository<T> where T : class, new()
    {

        ApplicationDbContext _c = new ApplicationDbContext();
        public void TAdd(T p)
        {
            _c.Set<T>().Add(p);
            _c.SaveChanges();
        }
        public void TDelete(T p)
        {
            _c.Set<T>().Remove(p);
            _c.SaveChanges();
        }
        public void TUpdate(T p)
        {
            _c.Set<T>().Update(p);
            _c.SaveChanges();
        }

        public List<T> TList()
        {
            return _c.Set<T>().ToList();
        }
        public T TGet(int id)
        {

            return _c.Set<T>().Find(id);
        }
    }
}
