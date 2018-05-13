using System.ComponentModel.DataAnnotations;

namespace brewtodo.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        
        public string ReviewDescription { get; set; }
        public float Rating { get; set; }
        public int UserID { get; set; }
        public int BreweryID { get; set; }

        public virtual User User { get; set; }
        public virtual Brewery Brewery { get; set; }
    }
}