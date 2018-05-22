using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Models
{
    public class UserPurchasedItemViewModel
    {
        public int UserPurchasedItemID { get; set; } 
        public int BreweryID { get; set; }
        public bool PurchasedTShirt { get; set; }
        public bool PurchasedMug { get; set; }
        public bool PurchasedGrowler { get; set; }
        public bool TriedFood { get; set; }
    }
}