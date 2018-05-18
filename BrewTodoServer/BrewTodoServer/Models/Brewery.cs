using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class Brewery
    {
        public int BreweryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public int StateID { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessHours { get; set; }
        public bool HasTShirt { get; set; }
        public bool HasMug { get; set; }
        public bool HasGrowler { get; set; }
        public bool HasFood { get; set; }


        public virtual State State { get; set; }
    }
}