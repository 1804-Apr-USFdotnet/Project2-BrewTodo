﻿using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security;
using System.Security.Claims;
using BrewTodoServer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BrewTodoServer.Data;

namespace BrewTodoServer.Controllers
{
    public class AccountController : ApiController
    {
        private UserRepository _context = new UserRepository();

        [HttpDelete]
        [Route("~/api/Account/Delete")]
        [AllowAnonymous]
        public IHttpActionResult Remove(int id)
        {
            User user = _context.Get(id);
            if(user != null)
            {
                var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new DbContext()));

                IdentityUser identityUser = userManager.Users.FirstOrDefault(x => user.IdentityID == x.Id);
                if(identityUser != null)
                {
                    userManager.Delete(identityUser);
                    _context.Delete(id);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("~/api/Account/Register")]
        [AllowAnonymous]
        public IHttpActionResult Register(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // actually register
            var userStore = new UserStore<IdentityUser>(new DbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(account.UserName);
            

            if (userManager.Users.Any(u => u.UserName == account.UserName))
            {
                return BadRequest("Username already exists");
            }

            userManager.Create(user, account.Password);
            var userID = user.Id;

            var userCustomDb = new User { Username = account.UserName, IdentityID = userID };
            _context.Post(userCustomDb);

            return Ok();
        }

        [HttpPost]
        [Route("~/api/Account/RegisterAdmin")]
        [AllowAnonymous]
        public IHttpActionResult RegisterAdmin(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // actually register
            var userStore = new UserStore<IdentityUser>(new DbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser(account.UserName);

            if (userManager.Users.Any(u => u.UserName == account.UserName))
            {
                return BadRequest();
            }

            userManager.Create(user, account.Password);

            // the only difference from Register action
            userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "admin"));

            return Ok();
        }

        [HttpPost]
        [Route("~/api/Account/Login")]
        [AllowAnonymous]
        public IHttpActionResult LogIn(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // actually login
            var userStore = new UserStore<IdentityUser>(new DbContext());
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Users.FirstOrDefault(u => u.UserName == account.UserName);

            if (user == null)
            {
                return BadRequest();
            }

            if (!userManager.CheckPassword(user, account.Password))
            {
                return Unauthorized();
            }

            var authManager = Request.GetOwinContext().Authentication;
            var claimsIdentity = userManager.CreateIdentity(user, WebApiConfig.AuthenticationType);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);

            return Ok();
        }

        [HttpGet]
        [Route("~/api/Account/Logout")]
        public IHttpActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(WebApiConfig.AuthenticationType);
            return Ok();
        }
    }
}
