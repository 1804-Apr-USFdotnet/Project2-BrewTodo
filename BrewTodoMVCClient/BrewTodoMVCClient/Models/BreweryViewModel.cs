using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Models
{
    public class BreweryViewModel
    {
        public int BreweryID { get; set; }
        public string Name {get; set;}
        public string Description {get; set;}
        public string ImageURL {get; set;}
        public string Address {get; set;}
        public string ZipCode{get; set;}
        public int StateID { get; set; }
        public string PhoneNumber {get; set;}
        public string BusinessHours { get; set; }
        public bool HasTShirt {get; set;}
        public bool HasMug {get; set;}
        public bool HasGrowler {get; set;}
        public bool HasFood {get; set;}
        public double AverageRating { get; set; }

        public State State { get; set; }

        public ICollection<ReviewViewModel> Reviews { get; set; }
        public ICollection<BeerViewModel> Beers { get; set; }
    }
}