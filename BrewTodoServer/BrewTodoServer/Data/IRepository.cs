using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoServer.Data
{
    interface IRepository<T> 
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Put(int id);
        void Delete(int id);
        void Post();
    }
}
