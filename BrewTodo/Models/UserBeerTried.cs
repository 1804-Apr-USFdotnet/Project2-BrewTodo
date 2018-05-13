namespace brewtodo.Models
{
    public class UserBeerTried
    {

        public int UserBeerTriedID { get; set; }
        public int UserID { get; set; }
        public int BreweryID { get; set; }

        public virtual Brewery Brewery { get; set; }
        public virtual User User { get; set; }
    }
}