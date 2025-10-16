using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopify.Services
{
    public interface IRepository<T>             // Generic repository interface because 1 he repository use kar k mai saare class ko use kar sakta hu alag alag banan nahi padega 
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        List<T> GetAll();
    }
}
