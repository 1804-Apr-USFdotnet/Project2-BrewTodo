using BrewTodoMVCClient.Logic;
using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BrewTodoMVCClient.Controllers
{
    public class UserController : Controller
    {
        //GET: Users
        public ActionResult Users()
        {
            UserLogic userLogic = new UserLogic();
            ICollection<UserViewModel> users = userLogic.GetUsers();
            ViewBag.UserID = CurrentUser.currentUserId;
            ViewBag.HasCookie = userLogic.CheckForCookie();
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View(users);
        }
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Users");
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            UserLogic userLogic = new UserLogic();
            UserViewModel user = userLogic.GetUser(id);
            ViewBag.UserID = CurrentUser.currentUserId;
            ViewBag.HasCookie = userLogic.CheckForCookie();
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            AccountLogic accLogic = new AccountLogic();
            UserLogic userLogic = new UserLogic();
            if (ModelState.IsValid)
            {
                try
                {
                    UserViewModel user = new UserViewModel
                    {
                        FirstName = collection["FirstName"],
                        LastName = collection["LastName"],
                        Username = collection["UserName"]
                    };
                    var passMatch = userLogic.DoPasswordsMatch(collection["Password"], collection["Password2"]);
                    if (!passMatch)
                    {
                        ViewBag.PasswordError = "Passwords must match.";
                        return View(user);
                    }
                    List<UserViewModel> currentUsers = (List<UserViewModel>)userLogic.GetUsers();
                    var userExists = userLogic.UserAlreadyExists(user, currentUsers);
                    if (userExists)
                    {
                        return View("Username Already Exists");
                    }
                    Account account = new Account
                    {
                        Username = user.Username,
                        Password = collection["Password"]
                    };
                    accLogic.PostAccount(account);
                    var tempUsers = userLogic.GetUsers();
                    var tempUser = tempUsers.Where(x => x.Username.Equals(user.Username)).FirstOrDefault();
                    tempUser.FirstName = user.FirstName;
                    tempUser.LastName = user.LastName;
                    userLogic.PutUser(tempUser);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Caught Exception");
                }
            }
            else
            {
                return View("Invalid Model State");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {

            UserLogic userLogic = new UserLogic();
            UserViewModel user;
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            if (!userLogic.CheckForCookie())
            {
                return RedirectToAction("Login", "Account");
            }
            if (userLogic.UserIdsMatch((int)CurrentUser.currentUserId, (int)id))
            {
                user = userLogic.GetUser((int)id);
                return View(user);
            }
            else
            {
                return RedirectToAction("Users");
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            UserLogic userLogic = new UserLogic();
            if (ModelState.IsValid)
            {
                try
                {
                    UserViewModel user = userLogic.GetUser(id);
                    user.FirstName = collection["FirstName"];
                    user.LastName = collection["LastName"];
                    userLogic.PutUser(user);
                    if (!userLogic.CheckForCookie())
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Caught Exception");
                }
            }
            else
            {
                return View("Invalid Model State");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            UserLogic userLogic = new UserLogic();
            UserViewModel user;
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            if (!userLogic.CheckForCookie())
            {
                return RedirectToAction("Login", "Account");
            }
            if (userLogic.UserIdsMatch((int)CurrentUser.currentUserId, (int)id))
            {
                user = userLogic.GetUser((int)id);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            UserLogic userLogic = new UserLogic();
            var user = userLogic.GetUser(id);
            try
            {
                if (!userLogic.CheckForCookie())
                {
                    return RedirectToAction("Login", "Account");
                }
                userLogic.DeleteUser(user);
                
                return RedirectToAction("Logout", "Account");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
