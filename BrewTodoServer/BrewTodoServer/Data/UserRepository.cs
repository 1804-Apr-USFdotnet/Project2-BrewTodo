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
        private DbContext db = new DbContext();

        public UserRepository(DbContext db)
        {
            this.db = db;
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
        
        public bool Delete(int id)
        {
            User brewery = db.Users.Find(id);
            if (brewery == null)
            {
                return false;
            }

            db.Users.Remove(brewery);
            db.SaveChanges();

            return true;

        }

        public IQueryable<User> Get()
        {
            return db.Users;
        }

        public User Get(int id)
        {

            User brewery = db.Users.Find(id);
            if (brewery == null)
            {
                return null;
            }
            return brewery;
        }

        public void Post(User brewery)
        {
            db.Users.Add(brewery);
            db.SaveChanges();
        }

        public bool Put(int id, User brewery)
        {
            if (db.Users.Where(a => a.UserID == id).FirstOrDefault() == null)
            {
                return false;
            }

            brewery.UserID = id;
            User oldBrew = db.Users.Where(a => a.UserID == id).FirstOrDefault();
            db.Entry(oldBrew).CurrentValues.SetValues(brewery);

            try
            {
                db.SaveChanges();
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
            ((IDisposable)db).Dispose();
        }
    }
}