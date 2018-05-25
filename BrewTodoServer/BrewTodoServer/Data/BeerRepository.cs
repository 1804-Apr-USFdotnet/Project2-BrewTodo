using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BrewTodoServer.Models;
using System.Data.Entity.Infrastructure;

namespace BrewTodoServer.Data
{
    public class BeerRepository : IRepository<Beer>, IDisposable
    {
        private DbContext _context;
        
        public BeerRepository(DbContext db = null)
        {
            _context = db ?? new DbContext();
        }

        private bool BeerExists(int id)
        {
            return _context.Beers.Count(e => e.BeerID == id) > 0;
        }

        public bool Delete(int id)
        {
            Beer Beer = _context.Beers.Find(id);
            if (Beer == null)
            {
                return false;
            }

            _context.Beers.Remove(Beer);
            _context.SaveChanges();

            return true;

        }

        public IQueryable<Beer> Get()
        {
            return _context.Beers;
        }

        public Beer Get(int id)
        {

            Beer Beer = _context.Beers.Find(id);
            if (Beer == null)
            {
                return null;
            }
            return Beer;
        }

        public void Post(Beer Beer)
        {
            _context.Beers.Add(Beer);
            _context.SaveChanges();
        }

        public bool Put(int id, Beer Beer)
        {
            if (_context.Beers.Where(a => a.BeerID == id).FirstOrDefault() == null)
            {
                return false;
            }

            Beer.BeerID = id;
            Beer oldBrew = _context.Beers.Where(a => a.BeerID == id).FirstOrDefault();
            _context.Entry(oldBrew).CurrentValues.SetValues(Beer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
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
            ((IDisposable)_context).Dispose();
        }
    }
}