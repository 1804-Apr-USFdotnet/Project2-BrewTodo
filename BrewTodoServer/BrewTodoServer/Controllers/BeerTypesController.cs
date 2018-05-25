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
    public class BeerTypesController : ApiController
    {
        private BeerTypeRepository _context = new BeerTypeRepository();
        //private DbContext db = new DbContext();
        // GET: api/BeerTypes
        public IQueryable<BeerType> GetBeerTypes()
        {
            return _context.Get();
        }

        // GET: api/BeerTypes/5
        [ResponseType(typeof(BeerType))]
        public IHttpActionResult GetBeerType(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/BeerTypes/5
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult PutBeerType(int id, BeerType beerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, beerType);
            if (result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);


        }

        // POST: api/BeerTypes
        [ResponseType(typeof(BeerType))]
        //[Authorize]
        public IHttpActionResult PostBeerType(BeerType beerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Post(beerType);
            return CreatedAtRoute("DefaultApi", new { id = beerType.BeerTypeID }, beerType);
        }

        // DELETE: api/BeerTypes/5
        [ResponseType(typeof(BeerType))]
        //[Authorize]
        public IHttpActionResult DeleteBeerType(int id)
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