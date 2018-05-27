using BrewTodoMVCClient;
using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoMVCClientTests.DummyClasses
{
    class TestReviewApiMethods : BrewTodoMVCClient.Logic.IApiMethods
    {
        private List<ReviewViewModel> list;

        public TestReviewApiMethods()
        {
            list = new List<ReviewViewModel>();
        }

        public void HttpDeleteFromApi<T>(string apiString, int id)
        {
            ReviewViewModel target = list.Where(x => x.ReviewID == id).FirstOrDefault();
            if (target != null)
            {
                list.Remove(target);
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public ICollection<T> HttpGetFromApi<T>(string apiString)
        {
            return list as ICollection<T>;
        }

        public void HttpPostToApi<T>(T model, string apiString)
        {
            ReviewViewModel review = model as ReviewViewModel;

            if (review.Rating >= 0 && 
                review.UserID > 0 &&
                review.BreweryID > 0)
            {
                list.Add(model as ReviewViewModel);
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public void HttpPutToApi<T>(T model, string apiString, int id)
        {
            ReviewViewModel review = model as ReviewViewModel;
            ReviewViewModel target = list.Where(x => x.ReviewID == id).FirstOrDefault();

            if (target != null &&
                review.Rating >= 0 &&
                review.UserID > 0 &&
                review.BreweryID > 0)
            {
                target.BreweryID = review.BreweryID;
                target.Rating = review.Rating;
                target.ReviewDescription = review.ReviewDescription;
                target.ReviewID = review.ReviewID;
                target.UserID = review.UserID;
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }
    }
}
