using BrewTodo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace brewtodo.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string IdentityID { get; set; }
        
        public string Username { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public byte[] ProfileImage { get; set; }

        
    }
}