using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class State
    {
        [JsonIgnore]
        public int StateID { get; set; }
        public string StateAbbr { get; set; }
    }
}