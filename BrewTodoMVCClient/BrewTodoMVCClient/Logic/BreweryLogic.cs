using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrewTodoMVCClient.Logic
{
    public class BreweryLogic
    {
        IApiMethods api;

        public BreweryLogic()
        {
            api = new ApiMethods();
        }

        public BreweryLogic(IApiMethods api)
        {
            this.api = api;
        }

        //Specific to BreweryLogic
        public ICollection<BreweryViewModel> GetBreweries()
        {
            ICollection<BreweryViewModel> breweries = api.HttpGetFromApi<BreweryViewModel>("breweries");
            return breweries;
        }
        public BreweryViewModel GetBrewery(int id)
        {
            IList<BreweryViewModel> breweries = (List<BreweryViewModel>)GetBreweries();
            var brewery = breweries.Where(x => x.BreweryID == id).FirstOrDefault();
            return brewery;
        }
        public void PostBrewery(BreweryViewModel brewery)
        {
            try
            {
                api.HttpPostToApi<BreweryViewModel>(brewery, "breweries");
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
        public void PutBrewery(BreweryViewModel brewery)
        {
            try
            {
                api.HttpPutToApi<BreweryViewModel>(brewery, "breweries",brewery.BreweryID);
            }
            catch (NonSuccessStatusCodeException e)
            {
                Console.WriteLine($"Exception caught: {e}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught: {e}");
            }
        }
        public void DeleteBrewery(int id)
        {
            try
            {
                api.HttpDeleteFromApi<BreweryViewModel>("breweries", id);
            }
            catch (NonSuccessStatusCodeException e)
            {
                Console.WriteLine($"Exception caught: {e}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught: {e}");
            }
        }
    }
}


