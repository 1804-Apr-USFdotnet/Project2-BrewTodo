using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BrewTodoMVCClient.Logic;

namespace BrewTodoMVCClient.Controllers
{
    public class BreweryController : Controller
    {
        private readonly BreweryLogic logic;

        public BreweryController()
        {
            logic = new BreweryLogic();
        }
        public BreweryController(BreweryLogic customLogic)
        {
            logic = customLogic;
        }

        // GET: Breweries
        public ActionResult Index()
        {
            return RedirectToAction("Breweries");
        }
        public ActionResult Breweries()
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View(logic.GetBreweries());
        }

        public ActionResult Create()
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View();
        }
        //POST: Brewery
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var state = new State
                    {
                        StateID = 10,
                        StateAbbr = "Fl"
                    };
                    BreweryViewModel brewery = new BreweryViewModel
                    {
                        Name = collection["Name"],
                        Description = collection["Description"],
                        ImageURL = collection["ImageURL"],
                        Address = collection["Address"],
                        ZipCode = collection["ZipCode"],
                        StateID = 10, //They should only be able to pick florida right now. Maybe still show the text field but make it uneditable
                        PhoneNumber = collection["PhoneNumber"],
                        BusinessHours = collection["BusinessHours"],
                        HasTShirt = Convert.ToBoolean(collection["HasTShirt"].Split(',')[0]),
                        HasMug = Convert.ToBoolean(collection["HasMug"].Split(',')[0]),
                        HasGrowler = Convert.ToBoolean(collection["HasGrowler"].Split(',')[0]),
                        HasFood = Convert.ToBoolean(collection["HasFood"].Split(',')[0]),
                        AverageRating = 0,
                        State = state
                    };
                    logic.PostBrewery(brewery);
                    return RedirectToAction("Breweries");
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
        //PUT: Brewery
        [HttpPost]
        public ActionResult Edit(int id,FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var state = new State
                    {
                        StateID = 10,
                        StateAbbr = "Fl"
                    };
                    BreweryViewModel brewery = new BreweryViewModel
                    {
                        BreweryID = id,
                        Name = collection["Name"],
                        Description = collection["Description"],
                        ImageURL = collection["ImageURL"],
                        Address = collection["Address"],
                        ZipCode = collection["ZipCode"],
                        StateID = 10, //They should only be able to pick florida right now. Maybe still show the text field but make it uneditable
                        PhoneNumber = collection["PhoneNumber"],
                        BusinessHours = collection["BusinessHours"],
                        HasTShirt = Convert.ToBoolean(collection["HasTShirt"].Split(',')[0]),
                        HasMug = Convert.ToBoolean(collection["HasMug"].Split(',')[0]),
                        HasGrowler = Convert.ToBoolean(collection["HasGrowler"].Split(',')[0]),
                        HasFood = Convert.ToBoolean(collection["HasFood"].Split(',')[0]),
                        State = state
                    };
                    logic.PutBrewery(brewery);
                    return RedirectToAction("Breweries");
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
        public ActionResult Edit(int id)
        {
            BreweryViewModel brewery = logic.GetBrewery(id);
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View(brewery);
        }
        public ActionResult Delete(int id)
        {
            BreweryViewModel brewery = logic.GetBrewery(id);
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View(brewery);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                logic.DeleteBrewery(id);
                return RedirectToAction("Breweries");
            }
            catch (Exception e)
            {
                return View("Caught Exception");
            }
        }
        //POST: Brewery
        public ActionResult Details(int? id)
        {
            BreweryViewModel brewery = logic.GetBrewery((int)id);
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View(brewery);
        }
    }
}