using BrewTodo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace brewtodo.Models
{
    public class User
    {
        [Key,ForeignKey("ApplicationUser")]
        public string UsersID { get; set; }
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        public byte[] ProfileImageURL { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}