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
    public class UsersController : ApiController
    {
        private UserRepository _context = new UserRepository(new DbContext());
        //private DbContext db = new DbContext();
        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return _context.Get();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, user);
            if (result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);

            
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        [Authorize]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Post(user);
            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [Authorize]
        public IHttpActionResult DeleteUser(int id)
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