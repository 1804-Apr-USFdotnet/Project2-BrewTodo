using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BrewTodoServer.Models;
using System.Data.Entity.Infrastructure;

namespace BrewTodoServer.Data
{
    public class BeerTypeRepository : IRepository<BeerType>, IDisposable
    {
        private DbContext _context;
        
        public BeerTypeRepository(DbContext db = null)
        {
            _context = db ?? new DbContext();
        }

        private bool BeerTypeExists(int id)
        {
            return _context.BeerTypes.Count(e => e.BeerTypeID == id) > 0;
        }

        public bool Delete(int id)
        {
            BeerType BeerType = _context.BeerTypes.Find(id);
            if (BeerType == null)
            {
                return false;
            }

            _context.BeerTypes.Remove(BeerType);
            _context.SaveChanges();

            return true;

        }

        public IQueryable<BeerType> Get()
        {
            return _context.BeerTypes;
        }

        public BeerType Get(int id)
        {

            BeerType BeerType = _context.BeerTypes.Find(id);
            if (BeerType == null)
            {
                return null;
            }
            return BeerType;
        }

        public void Post(BeerType BeerType)
        {
            _context.BeerTypes.Add(BeerType);
            _context.SaveChanges();
        }

        public bool Put(int id, BeerType BeerType)
        {
            if (_context.BeerTypes.Where(a => a.BeerTypeID == id).FirstOrDefault() == null)
            {
                return false;
            }

            BeerType.BeerTypeID = id;
            BeerType oldBrew = _context.BeerTypes.Where(a => a.BeerTypeID == id).FirstOrDefault();
            _context.Entry(oldBrew).CurrentValues.SetValues(BeerType);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerTypeExists(id))
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