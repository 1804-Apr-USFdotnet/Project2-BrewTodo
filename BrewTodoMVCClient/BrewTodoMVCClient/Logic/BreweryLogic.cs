using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;


namespace BrewTodoMVCClient.Logic
{
    public class BreweryLogic
    {
        ApiMethods api = new ApiMethods();

        //Specific to BreweryLogic
        public ICollection<BreweryViewModel> GetBreweries()
        {
            ICollection<BreweryViewModel> breweries = api.HttpGetFromApi<BreweryViewModel>("breweries");
            return breweries;
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
        public void DeleteBrewery(BreweryViewModel brewery)
        {
            try
            {
                api.HttpDeleteFromApi<BreweryViewModel>(brewery, "breweries", brewery.BreweryID);
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


