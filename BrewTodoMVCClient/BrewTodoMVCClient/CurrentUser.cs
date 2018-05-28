using BrewTodoMVCClient.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient
{
    public static class CurrentUser
    {
        public static int? currentUserId = null;
        public static bool UserLoggedIn()
        {
            return ApiMethods.IsCookieNotNull();
        }
    }
}