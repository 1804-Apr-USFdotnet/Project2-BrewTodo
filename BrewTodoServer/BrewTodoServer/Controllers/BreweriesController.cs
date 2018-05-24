using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BrewTodoServer.Data;
using BrewTodoServer.Models;

namespace BrewTodoServer.Controllers
{
    public class BreweriesController : ApiController
    {
        private readonly BreweryRepository _context = new BreweryRepository();
        

        // GET: api/Breweries
        
        public IQueryable<Brewery> GetBreweries()
        {
            return _context.Get();
        }

        // GET: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult GetBrewery(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        // PUT: api/Breweries/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrewery(int id, Brewery brewery)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                if (!_context.Put(id, brewery))
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                logger.Info(e.StackTrace);
                return BadRequest();
            }

        }


        // POST: api/Breweries
        [Authorize]
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult PostBrewery(Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Post(brewery);
            return CreatedAtRoute("DefaultApi", new { id = brewery.BreweryID }, brewery);
        }

        // DELETE: api/Breweries/5
        [Authorize]
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult DeleteBrewery(int id)
        {
            if (!_context.Delete(id))
            {
                return NotFound();
            }
            return Ok();
        }
    }
}