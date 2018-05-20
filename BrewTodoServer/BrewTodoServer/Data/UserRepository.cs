using BrewTodoServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Data
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private DbContext _context;

        public UserRepository(DbContext db = null)
        {
            _context = db ?? new DbContext();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Count(e => e.UserID == id) > 0;
        }
        
        public bool Delete(int id)
        {
            User brewery = _context.Users.Find(id);
            if (brewery == null)
            {
                return false;
            }

            _context.Users.Remove(brewery);
            _context.SaveChanges();

            return true;

        }

        public IQueryable<User> Get()
        {
            return _context.Users;
        }

        public User Get(int id)
        {

            User brewery = _context.Users.Find(id);
            if (brewery == null)
            {
                return null;
            }
            return brewery;
        }

        public void Post(User brewery)
        {
            _context.Users.Add(brewery);
            _context.SaveChanges();
        }

        public bool Put(int id, User brewery)
        {
            if (_context.Users.Where(a => a.UserID == id).FirstOrDefault() == null)
            {
                return false;
            }

            brewery.UserID = id;
            User oldBrew = _context.Users.Where(a => a.UserID == id).FirstOrDefault();
            _context.Entry(oldBrew).CurrentValues.SetValues(brewery);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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