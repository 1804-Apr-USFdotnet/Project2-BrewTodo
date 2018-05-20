using BrewTodoServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Data
{
    public class ReviewRepository : IRepository<Review>, IDisposable
    {
        private DbContext db = new DbContext();

        public ReviewRepository(DbContext db)
        {
            this.db = db;
        }

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.ReviewID == id) > 0;
        }

        public bool Delete(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return false;
            }

            db.Reviews.Remove(review);
            db.SaveChanges();

            return true;

        }

        public IQueryable<Review> Get()
        {
            return db.Reviews;
        }

        public Review Get(int id)
        {

            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return null;
            }
            return review;
        }

        public void Post(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
        }

        public bool Put(int id, Review review)
        {
            if (db.Reviews.Where(a => a.ReviewID == id).FirstOrDefault() == null)
            {
                return false;
            }

            review.ReviewID = id;
            Review oldReview = db.Reviews.Where(a => a.ReviewID == id).FirstOrDefault();
            db.Entry(oldReview).CurrentValues.SetValues(review);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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