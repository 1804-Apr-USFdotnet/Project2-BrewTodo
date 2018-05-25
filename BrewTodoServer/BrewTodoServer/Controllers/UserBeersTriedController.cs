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
    public class UserBeersTriedController : ApiController
    {
        private UserBeerTriedRepository _context = new UserBeerTriedRepository();
        //private DbContext db = new DbContext();
        // GET: api/UserBeersTrieds
        public IQueryable<UserBeerTried> GetUserBeersTrieds()
        {
            return _context.Get();
        }

        // GET: api/UserBeersTrieds/5
        [ResponseType(typeof(UserBeerTried))]
        public IHttpActionResult GetUserBeersTried(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/UserBeersTrieds/5
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult PutUserBeersTried(int id, UserBeerTried UserBeersTried)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, UserBeersTried);
            if (result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);


        }

        // POST: api/UserBeersTrieds
        [ResponseType(typeof(UserBeerTried))]
        //[Authorize]
        public IHttpActionResult PostUserBeersTried(UserBeerTried UserBeersTried)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Post(UserBeersTried);
            return CreatedAtRoute("DefaultApi", new { id = UserBeersTried.UserBeerTriedID }, UserBeersTried);
        }

        // DELETE: api/UserBeersTrieds/5
        [ResponseType(typeof(UserBeerTried))]
        //[Authorize]
        public IHttpActionResult DeleteUserBeersTried(int id)
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