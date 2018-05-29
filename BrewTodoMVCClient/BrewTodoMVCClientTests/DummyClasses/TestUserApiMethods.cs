using BrewTodoMVCClient;
using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoMVCClientTests.DummyClasses
{
    class TestUserApiMethods : BrewTodoMVCClient.Logic.IApiMethods
    {
        private ICollection<Account> aList;
        private ICollection<UserViewModel> uList;
        public bool logIn;

        public TestUserApiMethods(ICollection<UserViewModel> uList, ICollection<Account> aList)
        {
            this.aList = aList;
            this.uList = uList;
        }

        public void HttpDeleteFromApi(string apiString, int id)
        {
            UserViewModel target = uList.Where(x => x.UserID == id).FirstOrDefault();
            if (target != null)
            {
                uList.Remove(target);
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public ICollection<T> HttpGetFromApi<T>(string apiString)
        {
            return uList as ICollection<T>;
        }

        public void HttpPostToApi<T>(T model, string apiString)
        {
            UserViewModel User = model as UserViewModel;

            if (User.Username != null)
            {
                uList.Add(model as UserViewModel);
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public void HttpPostToApi<T>(T model, string controller, string action)
        {
            HttpPostToApi(model, controller + action);
        }

        public void HttpPutToApi<T>(T model, string apiString, int id)
        {
            UserViewModel User = model as UserViewModel;
            UserViewModel target = uList.Where(x => x.UserID == id).FirstOrDefault();

            if (target != null &&
                User.Username != null)
            {
                target.Username = User.Username;
                target.UserID = User.UserID;
                target.FirstName = User.FirstName;
                target.LastName = User.LastName;
            }
            else
            {
                throw new NonSuccessStatusCodeException("Non-success Status Code returned");
            }
        }

        public bool IsCookieNotNull()
        {
            return logIn;
        }

        public void RemoveCookie()
        {
            throw new NotImplementedException();
        }
    }
}
