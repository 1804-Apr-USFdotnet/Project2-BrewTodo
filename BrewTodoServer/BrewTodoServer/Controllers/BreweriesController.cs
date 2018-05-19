using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BrewTodoServer;
using BrewTodoServer.Data;
using BrewTodoServer.Models;

namespace BrewTodoServer.Controllers
{
    public class BreweriesController : ApiController
    {
        private BreweryRepository _context = new BreweryRepository(new DbContext());
        

        // GET: api/Breweries
        //[Authorize]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, brewery);
            if(result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
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
            var result = _context.Delete(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}