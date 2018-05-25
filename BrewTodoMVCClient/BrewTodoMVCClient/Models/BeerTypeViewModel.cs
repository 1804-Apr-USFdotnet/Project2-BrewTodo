using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Models
{
    public class BeerTypeViewModel
    {
        [Display(Name = "ID")]
        public int BeerTypeID { get; set; }
        [Display(Name = "Beer Type")]
        public string BeerTypeName { get; set; }
    }
}