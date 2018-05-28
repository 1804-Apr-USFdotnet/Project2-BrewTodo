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
    public class BeersController : ApiController
    {
        private BeerRepository _context = new BeerRepository();
        //private DbContext db = new DbContext();
        // GET: api/Beers
        public IQueryable<Beer> GetBeers()
        {
            return _context.Get();
        }

        // GET: api/Beers/5
        [ResponseType(typeof(Beer))]
        public IHttpActionResult GetBeer(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Beers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBeer(int id, Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, beer);
            if (result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);


        }

        // POST: api/Beers
        [ResponseType(typeof(Beer))]
        public IHttpActionResult PostBeer(Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Post(beer);
            return CreatedAtRoute("DefaultApi", new { id = beer.BeerID }, beer);
        }

        // DELETE: api/Beers/5
        [ResponseType(typeof(Beer))]
        public IHttpActionResult DeleteBeer(int id)
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