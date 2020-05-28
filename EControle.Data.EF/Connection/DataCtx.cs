using EControle.Core.Data.Models;

namespace EControle.Data.EF.Connection
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataCtx : DbContext
    {
      
        public DataCtx()
            : base("name=DataCtx5")
        {
        }

        //public DbSet<Account> Accounts { get; set; }
        public DbSet<InternalTransaction> InternalTransactions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<SedcAccount> SedcAccounts { get; set; }

        public DbSet<BalanceSheet> BalanceSheets { get; set; }
        public DbSet<SEDCTransaction> SEDCTransaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder model)
        {
            //modelBuilder.Entity<User>()
            //    .HasMany<InternalTransaction>(u => u.Transactions)
            //    .WithMany()
            //base.OnModelCreating(model);
            //model.Entity<InternalTransaction>()
            //    .HasRequired<User>(t => t.User)
            //    .WithMany(x => x.Transactions)
            //    .HasForeignKey<long>(x => x.UserId);

        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}