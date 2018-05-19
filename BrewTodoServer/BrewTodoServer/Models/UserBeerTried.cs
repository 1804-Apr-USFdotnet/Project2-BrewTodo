﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrewTodoServer.Models
{
    public class UserBeerTried
    {
        [Required]
        public int UserBeerTriedID { get; set; }
        [Required]
        [JsonIgnore]
        public int UserID { get; set; }
        [Required]
        public int BeerID { get; set; }

        [JsonIgnore]
        public virtual Beer Beer { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}