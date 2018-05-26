using BrewTodoMVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Logic
{
    public class UserLogic
    {
        ApiMethods api = new ApiMethods();

        public ICollection<UserViewModel> GetUsers()
        {
            ICollection<UserViewModel> users = api.HttpGetFromApi<UserViewModel>("users");
            return users;
        }
        public UserViewModel GetUser(int id)
        {
            IList<UserViewModel> users = (List<UserViewModel>)GetUsers();
            var user = users.Where(x => x.UserID == id).FirstOrDefault();
            return user;
        }
        public void PostUser(UserViewModel user)
        {
            try
            {
                api.HttpPostToApi<UserViewModel>(user, "users");
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
        public void PutUser(UserViewModel user)
        {
            try
            {
                api.HttpPutToApi<UserViewModel>(user, "users", user.UserID);

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
        public void DeleteUser(UserViewModel user)
        {
            try
            {
                api.HttpDeleteFromApi<UserViewModel>(user, "users", user.UserID);
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

        public bool UserAlreadyExists(UserViewModel user, List<UserViewModel> currentUsers)
        {
            var result = currentUsers.Where(x => x.Username == user.Username).Any();
            return result;
        }
    }
}