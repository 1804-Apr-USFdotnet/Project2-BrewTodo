using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BrewTodoServer.Models;
using System.Data.Entity.Infrastructure;

namespace BrewTodoServer.Data
{
    public class BreweryRepository : IRepository<Brewery>, IDisposable
    {
        private DbContext db = new DbContext();
        
        public BreweryRepository(DbContext db)
        {
            this.db = db;
        }

        private bool BreweryExists(int id)
        {
            return db.Breweries.Count(e => e.BreweryID == id) > 0;
        }

        public bool Delete(int id)
        {
            Brewery brewery = db.Breweries.Find(id);
            if (brewery == null)
            {
                return false;
            }

            db.Breweries.Remove(brewery);
            db.SaveChanges();

            return true;

        }

        public IQueryable<Brewery> Get()
        {
            return db.Breweries;
        }

        public Brewery Get(int id)
        {

            Brewery brewery = db.Breweries.Find(id);
            if (brewery == null)
            {
                return null;
            }
            return brewery;
        }

        public void Post(Brewery brewery)
        {
            db.Breweries.Add(brewery);
            db.SaveChanges();
        }

        public bool Put(int id, Brewery brewery)
        {
            if (db.Breweries.Where(a => a.BreweryID == id).FirstOrDefault() == null)
            {
                return false;
            }

            brewery.BreweryID = id;
            Brewery oldBrew = db.Breweries.Where(a => a.BreweryID == id).FirstOrDefault();
            db.Entry(oldBrew).CurrentValues.SetValues(brewery);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public void Dispose()
        {
            ((IDisposable)db).Dispose();
        }
    }
}