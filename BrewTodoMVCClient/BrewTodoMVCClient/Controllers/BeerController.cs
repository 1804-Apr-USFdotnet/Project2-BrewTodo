using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BrewTodoMVCClient.Controllers
{
    public class BeerController : Controller
    {
        // GET: Beer
        public ActionResult Index()
        {
            return RedirectToAction("Beers");
        }

        public ActionResult Beers()
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            ViewBag.BeerTypes = GetBeerTypesDropDownList();
            ViewBag.Breweries = GetBreweryDropDownList();

            ICollection<BeerViewModel> beers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/Beers");
                var responseTask = client.GetAsync("Beers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BeerViewModel>>();
                    readTask.Wait();
                    beers = readTask.Result;
                }
                else
                {
                    beers = (ICollection<BeerViewModel>)Enumerable.Empty<BeerViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error, no Beers found.");

                }
            }
            return View(beers);
        }

        // GET: Beer/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            if (id != null)
            {
                ViewBag.BeerTypes = GetBeerTypesDropDownList();
                ViewBag.Breweries = GetBreweryDropDownList();
                return View(GetBeer(id.Value));
            }
            else
            {
                return RedirectToAction("Beers");
            }
        }

        // GET: Beer/Create
        public ActionResult Create(BeerViewModel beer)
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            ViewBag.BeerTypes = GetBeerTypesDropDownList();
            ViewBag.Breweries = GetBreweryDropDownList();

            if (beer == null)
            {
                return View();
            }
            else
            {
                return View(beer);
            }
        }

        // POST: Beer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BeerViewModel beer = new BeerViewModel
                    {
                        BeerName = collection["BeerName"],
                        ABV = double.Parse(collection["ABV"]),
                        Description = collection["Description"],
                        BeerTypeID = int.Parse(collection["BeerTypes"]),
                        BreweryID = int.Parse(collection["Breweries"])
                    };

                    using(HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/beers");
                        var postTask = client.PostAsJsonAsync<BeerViewModel>("beers", beer);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("Beer failed to create");
                        }
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

        // GET: Beer/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            ViewBag.BeerTypes = GetBeerTypesDropDownList();
            ViewBag.Breweries = GetBreweryDropDownList();

            if (id != null)
            {
                return View(GetBeer(id.Value));
            }
            else
            {
                return RedirectToAction("Beers");
            }
        }

        // POST: Beer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    BeerViewModel beer = GetBeer(id);
                    beer.BeerName = collection["BeerName"];
                    beer.ABV = double.Parse(collection["ABV"]);
                    beer.Description = collection["Description"];
                    beer.BeerTypeID = int.Parse(collection["BeerTypes"]);
                    beer.BreweryID = int.Parse(collection["Breweries"]);

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/beers");
                        var putTask = client.PutAsJsonAsync<BeerViewModel>($"beers/{id}", beer);
                        putTask.Wait();
                        
                        if (putTask.Result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("Beer failed to update");
                        }
                    }
                }
                catch
                {
                    return View("Exception caught");
                }
            }
            else
            {
                return View("Invalid Model State");
            }
        }

        // GET: Beer/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            if (id != null)
            {
                ViewBag.BeerTypes = GetBeerTypesDropDownList();
                ViewBag.Breweries = GetBreweryDropDownList();

                return View(GetBeer(id.Value));
            }
            else
            {
                return RedirectToAction("Beers");
            }
        }

        // POST: Beer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/beers");
                    var deleteTask = client.DeleteAsync($"beers/{id}");
                    deleteTask.Wait();

                    if (deleteTask.Result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Beer failed to delete");
                    }
                }
            }
            catch
            {
                return View("Exception caught");
            }
        }

        private BeerViewModel GetBeer(int id)
        {
            BeerViewModel beer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/Beers");
                var responseTask = client.GetAsync($"Beers/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BeerViewModel>();
                    readTask.Wait();
                    beer = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error, no beer found.");
                }
            }

            return beer;
        }

        private ICollection<SelectListItem> GetBreweryDropDownList()
        {
            ICollection<BreweryViewModel> breweries = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/Breweries");
                var responseTask = client.GetAsync("Breweries");
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

            List<SelectListItem> breweryList = new List<SelectListItem>();
            foreach (var i in breweries)
            {
                breweryList.Add(new SelectListItem { Text = i.Name, Value = i.BreweryID.ToString() });
            }

            return breweryList;
        }

        private ICollection<SelectListItem> GetBeerTypesDropDownList()
        {
            ICollection<BeerTypeViewModel> breweries = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/BeerTypes");
                var responseTask = client.GetAsync("BeerTypes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BeerTypeViewModel>>();
                    readTask.Wait();
                    breweries = readTask.Result;
                }
                else
                {
                    breweries = (ICollection<BeerTypeViewModel>)Enumerable.Empty<BeerTypeViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error, no beer types found.");
                }
            }

            List<SelectListItem> breweryList = new List<SelectListItem>();
            foreach (var i in breweries)
            {
                breweryList.Add(new SelectListItem { Text = i.BeerTypeName, Value = i.BeerTypeID.ToString() });
            }

            return breweryList;
        }
    }
}
