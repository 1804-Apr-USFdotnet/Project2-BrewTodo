using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BrewTodoServer.Models;

namespace BrewTodoServer.Data
{
    public class BreweryRepository : IRepository<Brewery>
    {
        private DbContext db = new DbContext();
        
        public BreweryRepository(DbContext db)
        {
            this.db = db;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brewery> Get()
        {
            return db.Breweries.ToList();
        }

        public Brewery Get(int id)
        {

            throw new NotImplementedException();
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Put(int id)
        {
            throw new NotImplementedException();
        }
    }
}