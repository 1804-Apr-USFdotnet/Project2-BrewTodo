using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class Beer
    {

        public int BeerID { get; set; }
        [Required]
        public string BeerName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double ABV { get; set; }
        [Required]
        public int BeerTypeID { get; set; }
        [Required]
        public int BreweryID { get; set; }

        public virtual BeerType BeerType { get; set; }
        public virtual Brewery Brewery { get; set; }
    }
}