﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewTodoMVCClient.Logic
{
    interface IApiMethods
    {
        ICollection<T> HttpGetFromApi<T>(string apiString);
        void HttpPostToApi<T>(T model, string apiString);
        void HttpPutToApi<T>(T model, string apiString, int id);
        void HttpDeleteFromApi<T>(T model, string apiString, int id);
    }
}