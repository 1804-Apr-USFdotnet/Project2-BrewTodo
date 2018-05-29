using BrewTodoMVCClient;
using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoMVCClientTests.DummyClasses
{
    class TestAccountApiMethods : BrewTodoMVCClient.Logic.IApiMethods
    {
        private ICollection<Account> aList;
        private ICollection<UserViewModel> uList;

        public TestAccountApiMethods(ICollection<Account> aList, ICollection<UserViewModel> uList)
        {
            this.uList = uList;
            this.aList = aList;
        }

        public void HttpDeleteFromApi(string apiString, string id)
        {
            Account account = aList.Where(x => x.Username.Equals(id)).FirstOrDefault();
            if(account != null)
            {
                aList.Remove(account);
            }
        }

        public void HttpDeleteFromApi(string apiString, int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> HttpGetFromApi<T>(string apiString)
        {
            return aList as ICollection<T>;
        }

        public void HttpPostToApi<T>(T model, string apiString)
        {
            Account account = model as Account;

            if (account.Username != null)
            {
                aList.Add(account);
                UserViewModel user = new UserViewModel
                {
                    Username = account.Username,
                    IdentityID = account.Username
                };
                uList.Add(user);
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
            throw new NotImplementedException();
        }

        public bool IsCookieNotNull()
        {
            throw new NotImplementedException();
        }

        public void RemoveCookie()
        {
            throw new NotImplementedException();
        }
    }
}
