using System.ComponentModel.DataAnnotations;

namespace brewtodo.Models
{
    public class Brewery
    {
        public int BreweryID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public int StateID { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string BusinessHours { get; set; }
        public bool HasTShirt { get; set; }
        public bool HasMug { get; set; }
        public bool HasGrowler { get; set; }
        public bool HasFood { get; set; }


        public virtual State State { get; set; }
    }
}