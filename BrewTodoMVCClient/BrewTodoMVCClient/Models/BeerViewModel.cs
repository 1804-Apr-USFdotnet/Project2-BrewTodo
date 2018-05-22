using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Models
{
    public class BeerViewModel
    {
        public int BeerID { get; set; }
        public string BeerName { get; set; }
        public string Description { get; set; }
        public double ABV { get; set; }
        public int BeerTypeID {get; set;}
        public int BreweryID {get; set;}
        public BeerTypeViewModel BeerType { get; set; }
        public BreweryViewModel Brewery { get; set; }
    }
}