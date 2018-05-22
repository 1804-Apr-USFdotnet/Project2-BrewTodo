using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace BrewTodoMVCClient.Controllers
{
    public class ServiceController : Controller
    {
        protected static readonly HttpClient HttpClient = new HttpClient(new HttpClientHandler() { UseCookies = false });
        internal static readonly Uri serviceUri = new Uri("http://ec2-18-222-156-248.us-east-2.compute.amazonaws.com/BrewTodoServer_deploy/");
        private static readonly string cookieName = "AuthTestCookie";

        protected HttpRequestMessage CreateRequestToService(HttpMethod method, string uri)
        {
            var apiRequest = new HttpRequestMessage(method, new Uri(serviceUri, uri));

            string cookieValue = Request.Cookies[cookieName]?.Value ?? ""; // ?. operator new in C# 7

            apiRequest.Headers.Add("Cookie", new CookieHeaderValue(cookieName, cookieValue).ToString());

            return apiRequest;
        }
    }
}