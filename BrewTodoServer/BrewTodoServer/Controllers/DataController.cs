using BrewTodoServer.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
                //var users = _context.Get().ToList();
                //var result = users.Where(r => r.IdentityID == User.Identity.GetUserId());
            

                List<string> roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();

                return Ok($"Authenticated {username},  with roles: [{string.Join(", ", roles)}]!");
            }
        }
    }
