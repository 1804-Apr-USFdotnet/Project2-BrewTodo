using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class Account
    {
        [JsonProperty("accountId")]
        [Key]
        public int AccountID { get; set; }
        [JsonProperty("username")]
        [Required]
        public string UserName { get; set; }
        [Required]
        [JsonProperty("password")]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 64 characters.")]
        public string Password { get; set; }
    }
}