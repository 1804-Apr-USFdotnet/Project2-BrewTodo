using BrewTodoMVCClient.Models;
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

        //POST: Brewery
        public ActionResult Details(int? id)
        {
            BreweryViewModel brewery;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + "/api/breweries/");
                //client.BaseAddress = new Uri("http://localhost:56198/api/breweries/");
                //HTTP GET
                var responseTask = client.GetAsync(""+id);
                responseTask.Wait();


                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BreweryViewModel>();
                    readTask.Wait();

                    brewery = readTask.Result;
                }
                else
                {
                    brewery = new BreweryViewModel();

                    ModelState.AddModelError(string.Empty, "Server error, no breweries found.");
                }
            }
            return View(brewery);
        }
        
    }
}