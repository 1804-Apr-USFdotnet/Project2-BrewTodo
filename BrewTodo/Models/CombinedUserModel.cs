using brewtodo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodo.Models
{
    public class CombinedUserModel
    {
        public virtual User User { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}