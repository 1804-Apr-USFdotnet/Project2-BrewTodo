using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Logic
{
    public class ReviewLogic
    {
        ApiMethods api = new ApiMethods();

        public ICollection<ReviewViewModel> GetReviews()
        {
            ICollection<ReviewViewModel> reviews = api.HttpGetFromApi<ReviewViewModel>("reviews");
            return reviews;
        }
        public ReviewViewModel GetReview(int id)
        {
            IList<ReviewViewModel> reviews = (List<ReviewViewModel>)GetReviews();
            var review = reviews.Where(x => x.ReviewID == id).FirstOrDefault();
            return review;
        }
        public void PostReview(ReviewViewModel review)
        {
            try
            {
                api.HttpPostToApi<ReviewViewModel>(review, "reviews");
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
        public void PutReview(ReviewViewModel review)
        {
            {
                try
                {
                    api.HttpPutToApi<ReviewViewModel>(review, "reviews", review.ReviewID);
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
        public void DeleteReview(ReviewViewModel review)
        {
            try
            {
                api.HttpDeleteFromApi<ReviewViewModel>(review, "reviews", review.ReviewID);
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