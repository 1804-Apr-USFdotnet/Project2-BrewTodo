using BrewTodoServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Data
{
    public class UserPurchasedItemRepository : IRepository<UserPurchasedItem>, IDisposable
    {
        private DbContext _context;

        public UserPurchasedItemRepository(DbContext db = null)
        {
            _context = db ?? new DbContext();
        }

        private bool UserPurchasedItemExists(int id)
        {
            return _context.UserPurchasedItems.Count(e => e.UserPurchasedItemID == id) > 0;
        }
        
        public bool Delete(int id)
        {
            UserPurchasedItem brewery = _context.UserPurchasedItems.Find(id);
            if (brewery == null)
            {
                return false;
            }

            _context.UserPurchasedItems.Remove(brewery);
            _context.SaveChanges();

            return true;

        }

        public IQueryable<UserPurchasedItem> Get()
        {
            return _context.UserPurchasedItems;
        }

        public UserPurchasedItem Get(int id)
        {

            UserPurchasedItem brewery = _context.UserPurchasedItems.Find(id);
            if (brewery == null)
            {
                return null;
            }
            return brewery;
        }

        public void Post(UserPurchasedItem brewery)
        {
            _context.UserPurchasedItems.Add(brewery);
            _context.SaveChanges();
        }

        public bool Put(int id, UserPurchasedItem brewery)
        {
            if (_context.UserPurchasedItems.Where(a => a.UserPurchasedItemID == id).FirstOrDefault() == null)
            {
                return false;
            }

            brewery.UserPurchasedItemID = id;
            UserPurchasedItem oldBrew = _context.UserPurchasedItems.Where(a => a.UserPurchasedItemID == id).FirstOrDefault();
            _context.Entry(oldBrew).CurrentValues.SetValues(brewery);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPurchasedItemExists(id))
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