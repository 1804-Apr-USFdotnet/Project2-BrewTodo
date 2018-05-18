using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class UserPurchasedItem
    {
        public int UserPurchasedItemID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int BreweryID { get; set; }
        public bool PurchasedTShirt { get; set; }
        public bool PurchasedMug { get; set; }
        public bool PurchasedGrowler { get; set; }
        public bool TriedFood { get; set; }
        public virtual Brewery Brewery { get; set; }
        public virtual User User { get; set; }
    }
}