using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        public string ReviewDescription { get; set; }
        [Required]
        public float Rating { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("Brewery")]
        public int BreweryID { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Brewery Brewery { get; set; }
    }
}