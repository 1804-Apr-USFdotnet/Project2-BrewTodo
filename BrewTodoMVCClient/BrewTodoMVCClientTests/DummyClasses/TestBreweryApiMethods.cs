using BrewTodoMVCClient;
using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoMVCClientTests.DummyClasses
{
    class TestBreweryApiMethods : BrewTodoMVCClient.Logic.IApiMethods
    {
        private List<BreweryViewModel> list;

        public TestBreweryApiMethods()
        {
            list = new List<BreweryViewModel>();
        }

        public void HttpDeleteFromApi(string apiString, int id)
        {
            BreweryViewModel target = list.Where(x => x.BreweryID == id).FirstOrDefault();
            if (target != null)
            {
                list.Remove(target);
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public void HttpDeleteFromApi(string apiString, string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> HttpGetFromApi<T>(string apiString)
        {
            return list as ICollection<T>;
        }

        public void HttpPostToApi<T>(T model, string apiString)
        {
            BreweryViewModel brewery = model as BreweryViewModel;

            if (brewery.Name != null && 
                brewery.Address != null &&
                brewery.ZipCode != null)
            {
                list.Add(model as BreweryViewModel);
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public void HttpPutToApi<T>(T model, string apiString, int id)
        {
            BreweryViewModel brewery = model as BreweryViewModel;
            BreweryViewModel target = list.Where(x => x.BreweryID == id).FirstOrDefault();

            if (target != null &&
                brewery.Name != null &&
                brewery.Address != null &&
                brewery.ZipCode != null)
            {
                target.Address = brewery.Address;
                target.AverageRating = brewery.AverageRating;
                target.Beers = brewery.Beers;
                target.BusinessHours = brewery.BusinessHours;
                target.Description = brewery.Description;
                target.HasFood = brewery.HasFood;
                target.HasGrowler = brewery.HasGrowler;
                target.HasMug = brewery.HasMug;
                target.HasTShirt = brewery.HasTShirt;
                target.ImageURL = brewery.ImageURL;
                target.Name = brewery.Name;
                target.PhoneNumber = brewery.PhoneNumber;
                target.Reviews = brewery.Reviews;
                target.State = brewery.State;
                target.ZipCode = brewery.ZipCode;
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }
    }
}
