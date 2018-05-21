using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BrewTodoServer.Data;
using BrewTodoServer.Models;

namespace BrewTodoServer.Controllers
{
    public class ReviewsController : ApiController
    {
        private ReviewRepository _context = new ReviewRepository();

        // GET: api/Reviews
        public IQueryable<Review> GetReviews()
        {
            return _context.Get();
        }

        // GET: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult GetReview(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReview(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, review);
            if (result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reviews
        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Post(review);

            return CreatedAtRoute("DefaultApi", new { id = review.ReviewID }, review);
        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id)
        {

            var result = _context.Delete(id);
            if (result == false)
            {
                return NotFound();
            }
            
            return Ok();
        }

       
    }
}