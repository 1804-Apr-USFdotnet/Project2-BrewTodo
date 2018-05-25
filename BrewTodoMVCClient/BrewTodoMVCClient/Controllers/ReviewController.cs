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
                userId = 1;  //This will obviously need to be changed
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

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) //The id parameter here refers to the REVIEW ID not the brewery Id
        {
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
                    UserViewModel user = null;
                    BreweryViewModel brewery;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/");
                        var responseTask = client.GetAsync($"users/{review.UserID}");
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


                        responseTask = client.GetAsync("breweries/" + review.BreweryID);
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
                    review.User = user;
                    review.Brewery = brewery;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/reviews/");
                        var putTask = client.PutAsJsonAsync<ReviewViewModel>($"{id}", review);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Details", "Brewery", new { id = brewery.BreweryID });
                        }
                        return View("Non-success Status Code returned");
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
