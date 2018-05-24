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
            ICollection<UserViewModel> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/users");
                var responseTask = client.GetAsync("users");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readTask.Wait();
                    users = readTask.Result;
                }
                else
                {
                    users = (ICollection<UserViewModel>)Enumerable.Empty<UserViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error, no users found.");

                }
            }
            return View(users);
        }

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Users");
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
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

        // GET: User/Create
        public ActionResult Create(UserViewModel user)
        {
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return View();
            }
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
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

                    if (!collection["Password"].Equals(collection["Password2"]))
                    {
                        ViewBag.PasswordError = "Passwords must match.";
                        return View(user);
                    }
                    else {
                        using (var client = new HttpClient())
                        {
                            Account account = new Account
                            {
                                Username = user.Username,
                                Password = collection["Password"]
                            };

                            client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/account");
                            var postTask = client.PostAsJsonAsync<Account>("account/register", account);
                            postTask.Wait();

                            if (postTask.Result.IsSuccessStatusCode)
                            {
                                using(var userClient = new HttpClient())
                                {
                                    userClient.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/users");
                                    var responseTask = userClient.GetAsync("users");
                                    responseTask.Wait();

                                    if(responseTask.Result.IsSuccessStatusCode)
                                    {
                                        var readTask = responseTask.Result.Content.ReadAsAsync<IList<UserViewModel>>();
                                        readTask.Wait();
                                        var users = readTask.Result;

                                        UserViewModel newUser = users.Where(x => x.Username == account.Username).Last();
                                        newUser.FirstName = collection["FirstName"];
                                        newUser.LastName = collection["LastName"];

                                        var userPostTask = userClient.PostAsJsonAsync($"users/{newUser.UserID}", newUser);
                                        userPostTask.Wait();

                                        if(userPostTask.Result.IsSuccessStatusCode)
                                        {
                                            return RedirectToAction("Index");
                                        }
                                        else
                                        {
                                            return View("Error");
                                        }
                                    }
                                }
                            }
                        }

                        return View("User failed to create");
                    }
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
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
