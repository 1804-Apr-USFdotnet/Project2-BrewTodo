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
        [Route("~/api/Account/Delete/{id}")]
        [AllowAnonymous]
        public IHttpActionResult Delete(int userID)
        {
            User user = _context.Get().Where(x => x.UserID == userID).FirstOrDefault();
            if (user != null)
            {
                var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new DbContext()));

                var idUser = userManager.FindById(user.IdentityID);
                if(idUser != null)
                {
                
                    userManager.Delete(idUser);
                    _context.Delete(user.UserID);

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

            
            string userID = user.Id;

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
