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
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
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
                    userLogic.PostUser(user);
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
            if (id != null)
            {
                return View(GetUser(id.Value));
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
            if (ModelState.IsValid)
            {
                try
                {
                    UserViewModel user = GetUser(id);
                    user.UserID = id;
                    user.FirstName = collection["FirstName"];
                    user.LastName = collection["LastName"];
                    
                    using (var userClient = new HttpClient())
                    {
                        userClient.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/users");
                        var responseTask = userClient.PutAsJsonAsync($"users/{id}", user);
                        responseTask.Wait();

                        if (responseTask.Result.IsSuccessStatusCode)
                        {
                                return RedirectToAction("Index");
                        }
                    }

                    return View("User failed to update");
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
            if (id != null)
            {
                return View(GetUser(id.Value));
            }
            else
            {
                return RedirectToAction("Users");
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var userClient = new HttpClient())
                {
                    userClient.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/users");
                    var responseTask = userClient.DeleteAsync($"users/{id}");
                    responseTask.Wait();

                    if (responseTask.Result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch
            {
                return View("Error");
            }
        }

        private UserViewModel GetUser(int id)
        {
            UserViewModel user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/users");
                var responseTask = client.GetAsync($"users/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserViewModel>();
                    readTask.Wait();
                    user = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error, no user found.");
                }
            }

            return user;
        }
    }
}
