using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string IdentityID { get; set; }
        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}