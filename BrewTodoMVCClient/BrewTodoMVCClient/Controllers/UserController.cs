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
        public ActionResult Details(int id)
        {
            return View(GetUser(id));
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
                            client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/users");
                            var postTask = client.PostAsJsonAsync<UserViewModel>("users", user);
                            postTask.Wait();

                            if (postTask.Result.IsSuccessStatusCode)
                            {
                                Account account = new Account
                                {
                                    Username = user.Username,
                                    Password = collection["Password"]
                                };

                                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/account");
                                postTask = client.PostAsJsonAsync<Account>("register", account);
                                postTask.Wait();

                                if (postTask.Result.IsSuccessStatusCode)
                                {
                                    return RedirectToAction("Users");
                                }
                                else
                                {
                                    client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/users");
                                    var responseTask = client.GetAsync("users");
                                    responseTask.Wait();

                                    var readTask = responseTask.Result.Content.ReadAsAsync<IList<UserViewModel>>();
                                    readTask.Wait();
                                    user = readTask.Result.Where(x => x.Username == collection["UserName"]).FirstOrDefault();

                                    client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/users");
                                    var deleteTask = client.PostAsJsonAsync<int>("delete", user.UserID);
                                    deleteTask.Wait();
                                    
                                    return View("Account failed to create");
                                }
                            }
                            else
                            {
                                return View("User failed to create");
                            }
                        }
                    }
                }
                catch (Exception e)
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
        public ActionResult Edit(int id)
        {
            return View(GetUser(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetUser(id));
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
