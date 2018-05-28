using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BrewTodoMVCClient.Controllers
{
    public class HomeController : ServiceController
    {
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage apiRequest = CreateRequestToService(HttpMethod.Get, "api/Data");

            HttpResponseMessage apiResponse;
            try
            {
                apiResponse = await HttpClient.SendAsync(apiRequest);
            }
            catch
            {
                return View("Error");
            }

            
            //if (apiResponse.StatusCode != HttpStatusCode.Unauthorized)
            //{
            //    return View("Error");
            //}
                
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.LogIn = CurrentUser.UserLoggedIn();
            return View();
        }
    }
}
