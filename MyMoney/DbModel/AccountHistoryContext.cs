using System.Data.Entity;

namespace MyMoney.DbModel
{
    public class AccountHistoryContext : DbContext
    {
        public AccountHistoryContext() : base("DefaultConnection")
        {
            Database.SetInitializer<AccountHistoryContext>(null);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}


