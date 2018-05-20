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
        private DbContext _context;

        public ReviewRepository(DbContext db = null)
        {
            _context = db ?? new DbContext();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Count(e => e.ReviewID == id) > 0;
        }

        public bool Delete(int id)
        {
            Review review = _context.Reviews.Find(id);
            if (review == null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return true;

        }

        public IQueryable<Review> Get()
        {
            return _context.Reviews;
        }

        public Review Get(int id)
        {

            Review review = _context.Reviews.Find(id);
            if (review == null)
            {
                return null;
            }
            return review;
        }

        public void Post(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public bool Put(int id, Review review)
        {
            if (_context.Reviews.Where(a => a.ReviewID == id).FirstOrDefault() == null)
            {
                return false;
            }

            review.ReviewID = id;
            Review oldReview = _context.Reviews.Where(a => a.ReviewID == id).FirstOrDefault();
            _context.Entry(oldReview).CurrentValues.SetValues(review);

            try
            {
                _context.SaveChanges();
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
            ((IDisposable)_context).Dispose();
        }
    }
}