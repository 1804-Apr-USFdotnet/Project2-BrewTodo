using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoServer.Data
{
    public interface IRepository<T> 
    {
        IQueryable<T> Get();
        T Get(int id);
        bool Put(int id,T model);
        bool Delete(int id);
        void Post(T model);
    }
}
