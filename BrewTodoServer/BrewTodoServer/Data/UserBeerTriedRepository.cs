using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BrewTodoServer.Models;
using System.Data.Entity.Infrastructure;

namespace BrewTodoServer.Data
{
    public class UserBeerTriedRepository : IRepository<UserBeerTried>, IDisposable
    {
        private DbContext _context;
        
        public UserBeerTriedRepository(DbContext db = null)
        {
            _context = db ?? new DbContext();
        }

        private bool UserBeerTriedExists(int id)
        {
            return _context.UserBeersTried.Count(e => e.UserBeerTriedID == id) > 0;
        }

        public bool Delete(int id)
        {
            UserBeerTried UserBeerTried = _context.UserBeersTried.Find(id);
            if (UserBeerTried == null)
            {
                return false;
            }

            _context.UserBeersTried.Remove(UserBeerTried);
            _context.SaveChanges();

            return true;

        }

        public IQueryable<UserBeerTried> Get()
        {
            return _context.UserBeersTried;
        }

        public UserBeerTried Get(int id)
        {

            UserBeerTried UserBeerTried = _context.UserBeersTried.Find(id);
            if (UserBeerTried == null)
            {
                return null;
            }
            return UserBeerTried;
        }

        public void Post(UserBeerTried UserBeerTried)
        {
            _context.UserBeersTried.Add(UserBeerTried);
            _context.SaveChanges();
        }

        public bool Put(int id, UserBeerTried UserBeerTried)
        {
            if (_context.UserBeersTried.Where(a => a.UserBeerTriedID == id).FirstOrDefault() == null)
            {
                return false;
            }

            UserBeerTried.UserBeerTriedID = id;
            UserBeerTried oldBrew = _context.UserBeersTried.Where(a => a.UserBeerTriedID == id).FirstOrDefault();
            _context.Entry(oldBrew).CurrentValues.SetValues(UserBeerTried);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBeerTriedExists(id))
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