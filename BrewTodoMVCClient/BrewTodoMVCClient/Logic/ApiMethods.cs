using BrewTodoMVCClient.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace BrewTodoMVCClient.Logic
{
    public class ApiMethods : IApiMethods
    {
        public void RemoveCookie()
        {
            HttpContext.Current.Session.Remove("AuthTestCookie");
        }

        public static bool IsCookieNotNull()
        {
            return HttpContext.Current != null && HttpContext.Current.Request.Cookies["AuthTestCookie"] != null;
        }
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
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                string cookieValue;
                if (HttpContext.Current.Request.Cookies["AuthTestCookie"] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies["AuthTestCookie"].Value;
                    client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
                }
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"/api/{apiString}");
                var postTask = client.PostAsJsonAsync<T>($"{apiString}", model); //google how do i add cookie header to PostAsJsonAsync
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
        public void HttpPostToApi<T>(T model, string apiController, string apiAction)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                string cookieValue;
                if (HttpContext.Current.Request.Cookies["AuthTestCookie"] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies["AuthTestCookie"].Value;
                    client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
                }
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"/api/{apiController}");
                var postTask = client.PostAsJsonAsync<T>($"{apiAction}", model);
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
        public void HttpPutToApi<T>(T model, string apiString, int id)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                string cookieValue;
                if (HttpContext.Current.Request.Cookies["AuthTestCookie"] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies["AuthTestCookie"].Value;
                    client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
                }
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"api/{apiString}/");
                var postTask = client.PutAsJsonAsync<T>($"{id}", model);
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
        public void HttpDeleteFromApi(string apiString, int id)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                string cookieValue;
                if (HttpContext.Current.Request.Cookies["AuthTestCookie"] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies["AuthTestCookie"].Value;
                    client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
                }
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"api/{apiString}/");

                var deleteTask = client.DeleteAsync($"{id}");
                deleteTask.Wait();
                var result = deleteTask.Result;
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
        public void HttpDeleteFromApi(string apiString, string id)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                string cookieValue;
                if (HttpContext.Current.Request.Cookies["AuthTestCookie"] != null)
                {
                    cookieValue = HttpContext.Current.Request.Cookies["AuthTestCookie"].Value;
                    client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
                }
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"api/{apiString}/");
                var deleteTask = client.DeleteAsync($"{id}");
                deleteTask.Wait();
                var result = deleteTask.Result;
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
    }
}