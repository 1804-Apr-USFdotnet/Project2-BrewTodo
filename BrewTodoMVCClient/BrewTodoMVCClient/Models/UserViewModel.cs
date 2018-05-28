using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewTodoMVCClient.Models
{
    public class UserViewModel
    {
        
        public int UserID { get; set; }
        public string IdentityID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfileImage { get; set; }
        public ICollection<UserBeerTriedViewModel> UserBeersTried { get; set; }
        public ICollection<UserPurchasedItemViewModel> UserPurchasedItems { get; set; }
    }
}