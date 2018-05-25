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
    public class ReviewController : Controller
    {
        // GET: Review/Create/1   <--Brewery id
        public ActionResult Create(int id,int userId = 1) 
        {
            return View();
        }
        // POST: Review/Create/1 <--Brewery id
        [HttpPost]
        public ActionResult Create(FormCollection collection,int id,int? userId = 1) 
        {
            BreweryLogic brewLogic = new BreweryLogic();
            ReviewLogic revLogic = new ReviewLogic();
            BreweryViewModel brewery = brewLogic.GetBrewery(id);
            UserViewModel user = new UserViewModel
            {
                Username ="Dummy"
            };
            if (ModelState.IsValid)
            {
                try
                {
                    ReviewViewModel review = new ReviewViewModel
                    {
                        BreweryID = id,
                        Rating = Convert.ToInt32(collection["Rating"]),
                        ReviewDescription = collection["ReviewDescription"],
                        User = user,
                        UserID = (int)userId,
                        Brewery = brewery,
                    };
                    revLogic.PostReview(review);
                    return RedirectToAction("Details", "Brewery", new { id = id });
                }
                catch(Exception e)
                {
                    return View("Caught Exception");
                }
            }
            else
            {
                return View("Invalid Model State");
            }
        }

        // GET: Review/Edit/5 <-- Review id
        public ActionResult Edit(int id)
        {
            ReviewLogic revLogic = new ReviewLogic();
            ReviewViewModel review = revLogic.GetReview(id);
            return View(review);
        }

        // POST: Review/Edit/5 <-- Review id
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) //The id parameter here refers to the REVIEW ID not the brewery Id
        {
            BreweryLogic brewLogic = new BreweryLogic();
            ReviewLogic revLogic = new ReviewLogic();
            if (ModelState.IsValid)
            {
                try
                {
                    ReviewViewModel review = new ReviewViewModel
                    {
                        ReviewID = id,
                        ReviewDescription = collection["ReviewDescription"],
                        Rating = (float)Convert.ToDouble(collection["Rating"]),
                        UserID = Convert.ToInt32(collection["UserID"]),
                        BreweryID = Convert.ToInt32(collection["BreweryID"])
                    };
                    UserViewModel user = new UserViewModel
                    {
                        Username = "Dummy"
                    };
                    review.User = user;
                    BreweryViewModel brewery = brewLogic.GetBrewery(review.BreweryID);
                    review.Brewery = brewery;
                    revLogic.PutReview(review);
                    return RedirectToAction("Details", "Brewery", new { id = brewery.BreweryID });
                }
                catch(Exception)
                {
                    return View("Caught Exception");
                }
            }
            else
            {
                return View("Invalid Model State");
            }

        }

        // GET: Review/Delete/5 <-- Review id
        public ActionResult Delete(int id)
        {
            ReviewViewModel review = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/");
                var responseTask = client.GetAsync("reviews");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ReviewViewModel>>();
                    readTask.Wait();
                    review = readTask.Result.Where(x => x.ReviewID == id).FirstOrDefault();
                }
            }
            return View(review);
        }

        // POST: Review/Delete/5 <-- Review id  
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            
            using (var client = new HttpClient())
            {
                try
                {
                    ReviewViewModel review;

                    client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/");
                    var readTask = client.GetAsync($"reviews/{id}");
                    readTask.Wait();

                    var result = readTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var tempReview = result.Content.ReadAsAsync<ReviewViewModel>();
                        tempReview.Wait();
                        review = tempReview.Result;
                    }
                    else
                    {
                        review = new ReviewViewModel();

                        ModelState.AddModelError(string.Empty, "Server error, no review found.");
                    }
                    var deleteTask = client.DeleteAsync($"reviews/{id}");
                    deleteTask.Wait();

                    result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", "Brewery", new { id = review.BreweryID });
                    }
                    return View("Non-success Status Code returned");
                }
                catch(Exception e)
                {
                    return View("Caught Exception");
                }
            }
                
        }
    }
}
