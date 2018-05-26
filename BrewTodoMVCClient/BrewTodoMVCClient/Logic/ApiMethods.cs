using BrewTodoMVCClient.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BrewTodoMVCClient.Logic
{
    public class ApiMethods : IApiMethods
    {
        string cookieValue = "jIcdxxP-spgk4XGrFy_oV96EqwkF5OgRavyT5t84Zl7_xCkL3zWNMU67qc4actcBm4KJFcMa6hVObWxL3oRz2ct2owVLTCjAkeoPP6LKPo8s6k0wxAEiFWh-PVXZwfM7Lvofli5KIe7f4svMETDi8rZbeybDTu-voeR4q8809OrmgyjtBbao210KWbk2eWq0SxZ90lXPPQumnVm0AHRBaV6x_L788-2Rzjyu3xD2qxyyqtuRYnkclICupeosPo1qb2Xm09tmrd0i6qUOWNBoN2bpERZKzMrNrBTNBij3KtGOfIhtjq0D8QgMOlF6Coyaya7C7Vs8WyWZUUt2q30sq3GoGpprQYtmAh3q4VQfoOOEsXqE0fyBvDtL6vUawGRsarmcQUMhSzTj9jgVwZgpAQSjYHeVApYIeiAuUCLm89NJ-yzvO2ax7eufE2kxy-GefsYkx8Z8teuNkIZxWeJQxbH780ZrMjMLOZhvZ271T4FgA1t0wX1aORuACZdG5i08";
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
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"/api/{apiString}");
                client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
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
        public void HttpPostToApi<T>(T model, string apiController,string apiAction)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"/api/{apiController}");
                client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
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
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"api/{apiString}/");
                client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
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
        public void HttpDeleteFromApi<T>(T model, string apiString, int id)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false }))
            {
                client.BaseAddress = new Uri(ServiceController.serviceUri.ToString() + $"api/{apiString}/");
                client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue("AuthTestCookie", cookieValue).ToString());
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