using BrewTodoServer.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace BrewTodoServer.Controllers
{
    

    
        public class DataController : ApiController
        {
            private UserRepository _context = new UserRepository();

            public IHttpActionResult Get()
            {
                var user = Request.GetOwinContext().Authentication.User;

                string username = user.Identity.Name;

                bool isAdmin = user.IsInRole("admin");

                var id = user.Identity.GetUserId();

                var result = _context.Get().FirstOrDefault(r => r.IdentityID == id);

                List<string> roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();

           // return Ok($"Authenticated {username}, {result?.UserID} with roles: [{string.Join(", ", roles)}]!");
                return Ok(result?.UserID);
            }
        }
    }
