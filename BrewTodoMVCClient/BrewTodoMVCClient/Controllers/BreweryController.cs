﻿using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BrewTodoMVCClient.Controllers
{
    public class BreweryController : Controller
    {
        // GET: Breweries
        public ActionResult Breweries()
        {
            ICollection<BreweryViewModel> breweries = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString()+"/api/breweries");
                //client.BaseAddress = new Uri("http://localhost:56198/api/breweries/");
                //HTTP GET
                var responseTask = client.GetAsync("breweries");
                responseTask.Wait();
                

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BreweryViewModel>>();
                    readTask.Wait();

                    breweries = readTask.Result;
                }
                else
                {
                    breweries = (ICollection<BreweryViewModel>)Enumerable.Empty<BreweryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error, no breweries found.");
                }
            }
            return View(breweries);
        }

        public ActionResult CreateBrewery()
        {
            return View();
        }

        //POST: Brewery
        [HttpPost]
        public ActionResult CreateBrewery(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var state = new State
                    {
                        StateID = 1,
                        StateAbbr = "Fl"
                    };
                    BreweryViewModel brewery = new BreweryViewModel
                    {
                        Name = collection["Name"],
                        Description = collection["Description"],
                        ImageURL = collection["ImageURL"],
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

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/breweries");
                        var postTask = client.PostAsJsonAsync<BreweryViewModel>("breweries",brewery);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Breweries");
                        }
                        return View("Non-success Status Code returned");
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
        
    }
}