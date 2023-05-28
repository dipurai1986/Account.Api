using System;
using Microsoft.EntityFrameworkCore;
using Account.Domain;
namespace Account.Infrastructure.AccountDBContextNameSpace
{
    public class AccountDBContext: DbContext
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .HasKey(u => u.UserId);
            modelBuilder.Entity<UserAccount>()
                .HasKey(u => u.AccountId); 


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseInMemoryDatabase(databaseName: "AccountDB");
        }

        public DbSet<UserAccount> userAccounts { get; set; }
        public DbSet<User> user { get; set; }
    }
}