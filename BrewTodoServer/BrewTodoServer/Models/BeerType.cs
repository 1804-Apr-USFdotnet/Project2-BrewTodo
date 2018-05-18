using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class BeerType
    {
        public int BeerTypeID { get; set; }
        [Required]
        public string BeerTypeName { get; set; }
    }
}