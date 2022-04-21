using CustomerAndTransactionMgt.Models.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CustomerAndTransactionMgt.Data
{
    public class CustomerAndTransactionContext : DbContext
    {
        public CustomerAndTransactionContext(DbContextOptions<CustomerAndTransactionContext> options) : base(options)
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}

