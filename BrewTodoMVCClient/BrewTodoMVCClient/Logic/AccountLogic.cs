using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Logic
{
    public class AccountLogic
    {
        ApiMethods api = new ApiMethods();
        public void PostAccount(Account account)
        {
            try
            {
                api.HttpPostToApi<Account>(account, "Account/","Register");
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
        public void Logout()
        {
            api.RemoveCookie();
        }
        public bool UserIdsMatch(int userId, int accessId)
        {
            if (userId != accessId)
                return false;
            return true;
        }
    }
}