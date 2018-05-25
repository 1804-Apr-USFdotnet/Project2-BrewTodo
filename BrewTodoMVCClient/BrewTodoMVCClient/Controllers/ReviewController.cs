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
        // GET: Review/Create
        // in this case id parameter is the brewery id passed in the url
        //This is because in routConfig our only route is looking for a parameter specifically named 'id'
        public ActionResult Create(int id,int userId = 1) 
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection,int id,int? userId)
        {
            UserViewModel user = null;
            BreweryViewModel brewery;
            if(userId == null)
            {
                userId = 1;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/");
                var responseTask = client.GetAsync($"users/{userId}");
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
                

                responseTask = client.GetAsync("breweries/" + id);
                responseTask.Wait();
                result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BreweryViewModel>();
                    readTask.Wait();

                    brewery = readTask.Result;
                }
                else
                {
                    brewery = new BreweryViewModel();

                    ModelState.AddModelError(string.Empty, "Server error, no brewery found.");
                }
            }
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
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/");
                        var postTask = client.PostAsJsonAsync<ReviewViewModel>("reviews", review);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            //return RedirectToRoute("Details",id);
                            return RedirectToAction("Details", "Brewery", new { id = id });
                        }
                        return View("Non-success Statuse Code returned");
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View("Invalid Model State");
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Review/Edit/5
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

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
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
    }
}
