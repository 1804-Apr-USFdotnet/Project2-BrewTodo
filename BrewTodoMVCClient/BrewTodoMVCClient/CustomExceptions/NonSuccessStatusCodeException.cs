using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient
{
    public class NonSuccessStatusCodeException : Exception
    {
        public NonSuccessStatusCodeException(string message): base(message)
        {

        }
    }
}