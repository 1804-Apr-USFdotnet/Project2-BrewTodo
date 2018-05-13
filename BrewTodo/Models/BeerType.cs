using System.ComponentModel.DataAnnotations;

namespace brewtodo.Models
{
    public class BeerType
    {
        public int BeerTypeID { get; set; }
        [Required]
        public string BeerTypeName { get; set; }
    }
}