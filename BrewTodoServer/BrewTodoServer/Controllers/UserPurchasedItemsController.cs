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
    public class UserPurchasedItemsController : ApiController
    {
        private UserPurchasedItemRepository _context = new UserPurchasedItemRepository();
        //private DbContext db = new DbContext();
        // GET: api/UserPurchasedItems
        public IQueryable<UserPurchasedItem> GetUserPurchasedItems()
        {
            return _context.Get();
        }

        // GET: api/UserPurchasedItems/5
        [ResponseType(typeof(UserPurchasedItem))]
        public IHttpActionResult GetUserPurchasedItem(int id)
        {
            var result = _context.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/UserPurchasedItems/5
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult PutUserPurchasedItem(int id, UserPurchasedItem UserPurchasedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.Put(id, UserPurchasedItem);
            if (result == false)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);


        }

        // POST: api/UserPurchasedItems
        [ResponseType(typeof(UserPurchasedItem))]
        //[Authorize]
        public IHttpActionResult PostUserPurchasedItem(UserPurchasedItem UserPurchasedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Post(UserPurchasedItem);
            return CreatedAtRoute("DefaultApi", new { id = UserPurchasedItem.UserPurchasedItemID }, UserPurchasedItem);
        }

        // DELETE: api/UserPurchasedItems/5
        [ResponseType(typeof(UserPurchasedItem))]
        //[Authorize]
        public IHttpActionResult DeleteUserPurchasedItem(int id)
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