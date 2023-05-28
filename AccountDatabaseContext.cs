using System;

using Microsoft.EntityFrameworkCore;

namespace AccountDBContextNameSpace
{
    public class AccountDBContextName : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AccountDB");
        }

        public DbSet<Account> Authors { get; set; }
        public DbSet<User> user { get; set; }
    }
}