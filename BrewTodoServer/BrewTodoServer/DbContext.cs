using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using BrewTodoServer.Models;

namespace BrewTodoServer
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("BrewTodoDb") { }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BeerType> BeerTypes { get; set; }
        public DbSet<UserBeerTried> UserBeersTried { get; set; }
        public DbSet<UserPurchasedItem> UserPurchasedItems { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }

    }
}