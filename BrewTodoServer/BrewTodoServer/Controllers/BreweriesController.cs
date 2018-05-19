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
using BrewTodoServer.Models;

namespace BrewTodoServer.Controllers
{
    public class BreweriesController : ApiController
    {
        private DbContext db = new DbContext();

        // GET: api/Breweries
        //[Authorize]
        public IQueryable<Brewery> GetBreweries()
        {
            return db.Breweries;
        }

        // GET: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult GetBrewery(int id)
        {
            Brewery brewery = db.Breweries.Find(id);
            if (brewery == null)
            {
                return NotFound();
            }

            return Ok(brewery);
        }

        // PUT: api/Breweries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBrewery(int id, Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Breweries.Where(a => a.BreweryID == id).FirstOrDefault() == null)
            {
                return BadRequest();
            }

            brewery.BreweryID = id;
            //db.Entry(brewery).State = EntityState.Modified;

            Brewery oldBrew = db.Breweries.Where(a => a.BreweryID == id).FirstOrDefault();
            db.Entry(oldBrew).CurrentValues.SetValues(brewery);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Breweries
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult PostBrewery(Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            
            db.Breweries.Add(brewery);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brewery.BreweryID }, brewery);
        }

        // DELETE: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public IHttpActionResult DeleteBrewery(int id)
        {
            Brewery brewery = db.Breweries.Find(id);
            if (brewery == null)
            {
                return NotFound();
            }

            db.Breweries.Remove(brewery);
            db.SaveChanges();

            return Ok(brewery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreweryExists(int id)
        {
            return db.Breweries.Count(e => e.BreweryID == id) > 0;
        }
    }
}