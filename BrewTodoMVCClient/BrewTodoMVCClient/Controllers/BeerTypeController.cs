using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BrewTodoMVCClient.Controllers
{
    public class BeerTypeController : Controller
    {
        // GET: BeerType
        public ActionResult Index()
        {
            return RedirectToAction("BeerTypes");
        }

        public ActionResult BeerTypes()
        {
            ICollection<BeerTypeViewModel> beerTypes = null;

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
                    beerTypes = readTask.Result;
                }
                else
                {
                    beerTypes = (ICollection<BeerTypeViewModel>)Enumerable.Empty<BeerTypeViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error, no beer types found.");
                }
            }

            return View(beerTypes);
        }

        // GET: BeerType/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                return View(GetBeerType(id.Value));
            }
            else
            {
                return RedirectToAction("BeerTypes");
            }
        }

        // GET: BeerType/Create
        public ActionResult Create(BeerTypeViewModel beerType)
        {
            if (beerType == null)
            {
                return View();
            }
            else
            {
                return View(beerType);
            }
        }

        // POST: BeerType/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BeerTypeViewModel beerType = new BeerTypeViewModel
                    {
                        BeerTypeName = collection["BeerTypeName"]
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/BeerTypes");
                        var postTask = client.PostAsJsonAsync<BeerTypeViewModel>("BeerTypes", beerType);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("Beer type failed to create");
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

        // GET: BeerType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                return View(GetBeerType(id.Value));
            }
            else
            {
                return RedirectToAction("BeerTypes");
            }
        }

        // POST: BeerType/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BeerTypeViewModel beerType = GetBeerType(id);
                    beerType.BeerTypeName = collection["BeerTypeName"];

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/BeerTypes");
                        var putTask = client.PutAsJsonAsync<BeerTypeViewModel>($"BeerTypes/{id}", beerType);
                        putTask.Wait();

                        if (putTask.Result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("Beer type failed to update");
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

        // GET: BeerType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                return View(GetBeerType(id.Value));
            }
            else
            {
                return RedirectToAction("BeerTypes");
            }
        }

        // POST: BeerType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "api/BeerTypes");
                    var deleteTask = client.DeleteAsync($"BeerTypes/{id}");
                    deleteTask.Wait();

                    if (deleteTask.Result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Beer type failed to delete");
                    }
                }
            }
            catch
            {
                return View("Exception caught");
            }
        }

        private BeerTypeViewModel GetBeerType(int id)
        {
            BeerTypeViewModel beerType = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/BeerTypes");
                var responseTask = client.GetAsync($"BeerTypes/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BeerTypeViewModel>();
                    readTask.Wait();
                    beerType = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error, no beer type found.");
                }
            }

            return beerType;
        }
    }
}
