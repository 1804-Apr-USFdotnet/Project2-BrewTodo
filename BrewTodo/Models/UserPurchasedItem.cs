namespace brewtodo.Models
{
    public class UserPurchasedItem
    {
        public int UserPurchasedItemID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public Brewery Brewery { get; set; }
        public int BreweryID { get; set; }
        public bool PurchasedTShirt { get; set; }
        public bool PurchasedMug { get; set; }
        public bool PurchasedGrowler { get; set; }
        public bool TriedFood { get; set; }

    }
}