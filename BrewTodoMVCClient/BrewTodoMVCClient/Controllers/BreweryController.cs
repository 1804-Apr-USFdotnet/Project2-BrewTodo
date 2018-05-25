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
        // GET: Breweries
        public ActionResult Breweries()
        {
            BreweryLogic logic = new BreweryLogic();
            return View(logic.GetBreweries());
        }
        public ActionResult CreateBrewery()
        {
            return View();
        }
        //POST: Brewery
        [HttpPost]
        public ActionResult CreateBrewery(FormCollection collection)
        {
            BreweryLogic logic = new BreweryLogic();
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
        public ActionResult EditBrewery(int id,FormCollection collection)
        {
            BreweryLogic logic = new BreweryLogic();
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
        public ActionResult EditBrewery(int id)
        {
            BreweryLogic logic = new BreweryLogic();
            IList<BreweryViewModel> breweries = (List<BreweryViewModel>)logic.GetBreweries();
            BreweryViewModel brewery = breweries.Where(x => x.BreweryID == id).FirstOrDefault();
            return View(brewery);
        }
        public ActionResult DeleteBrewery(int id)
        {
            BreweryLogic logic = new BreweryLogic();
            IList<BreweryViewModel> breweries = (List<BreweryViewModel>)logic.GetBreweries();
            BreweryViewModel brewery = breweries.Where(x => x.BreweryID == id).FirstOrDefault();
            return View(brewery);
        }
        [HttpPost]
        public ActionResult DeleteBrewery(int id, FormCollection collection)
        {
            BreweryLogic logic = new BreweryLogic();
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
                    Name = "",
                    Description = "",
                    ImageURL = "",
                    Address = "",
                    ZipCode = "",
                    StateID = 10, //They should only be able to pick florida right now. Maybe still show the text field but make it uneditable
                    PhoneNumber = "",
                    BusinessHours = "",
                    HasTShirt = false,
                    HasMug = false,
                    HasGrowler = false,
                    HasFood = false,
                    State = state
                };
                logic.DeleteBrewery(brewery);
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
            BreweryLogic logic = new BreweryLogic();
            IList<BreweryViewModel> breweries = (List<BreweryViewModel>)logic.GetBreweries();
            BreweryViewModel brewery = breweries.Where(x => x.BreweryID == id).FirstOrDefault();
            return View(brewery);
        }
    }
}