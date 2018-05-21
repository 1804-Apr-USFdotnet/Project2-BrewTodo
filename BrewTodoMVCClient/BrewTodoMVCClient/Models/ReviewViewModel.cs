using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Models
{
    public class ReviewViewModel
    {
        public int ReviewID { get; set; }
        public string ReviewDescription { get; set; }
        public float Rating { get; set; }
        public int UserID { get; set; }
        public int BreweryID { get; set; }
        //TODO: implement UserViewModel
        //public UserViewModel User { get; set; }
        public BreweryViewModel Brewery { get; set; }
    }
}