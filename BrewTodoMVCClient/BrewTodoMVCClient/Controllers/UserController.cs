﻿using BrewTodoMVCClient.Logic;
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
                    userLogic.PutUser(user);
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
            if (!userLogic.CheckForCookie())
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != null)
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
            if (id != null)
            {
                user = userLogic.GetUser((int)id);
                return View(user);
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
            UserLogic userLogic = new UserLogic();
            var user = userLogic.GetUser(id);
            try
            {
                userLogic.DeleteUser(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
