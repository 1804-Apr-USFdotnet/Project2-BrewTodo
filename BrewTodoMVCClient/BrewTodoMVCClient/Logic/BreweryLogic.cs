using BrewTodoMVCClient.Controllers;
using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BrewTodoMVCClient.Logic
{
    public class BreweryLogic
    {
        //Might pull these out from BreweryLogic so that all logic classes can use it
        public ICollection<T> HttpGetFromApi<T>(string apiString)
        {
            ICollection<T> resultList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"/api/{apiString}");
                var responseTask = client.GetAsync($"{apiString}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ICollection<T>>();
                    readTask.Wait();
                    resultList = readTask.Result;
                }
                else
                {
                    resultList = (ICollection<T>)Enumerable.Empty<T>();
                }
                return resultList;
            }
        } 
        public void HttpPostToApi<T>(T model, string apiString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"/api/{apiString}");
                var postTask = client.PostAsJsonAsync<T>($"{apiString}", model);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    throw new NonSuccessStatusCodeException("Non-success Status Code returned");
                }
            }
        }

        //Specific to BreweryLogic
        public ICollection<BreweryViewModel> GetBreweries()
        {
            ICollection<BreweryViewModel> breweries = HttpGetFromApi<BreweryViewModel>("breweries");
            return breweries;
        }
        public void PostBrewery(BreweryViewModel brewery)
        {
            try
            {
                HttpPostToApi<BreweryViewModel>(brewery, "breweries");
            }
            catch(NonSuccessStatusCodeException e)
            {
                Console.WriteLine($"Exception caught: {e}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception caught: {e}");

            }
        }


    }
}


