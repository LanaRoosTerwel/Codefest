using Codefest.Models.Account;

using Microsoft.EntityFrameworkCore;

namespace Codefest
{
    public class CodefestContext : DbContext
    {
        public CodefestContext(DbContextOptions<CodefestContext> options) : base(options)
        { }
        
        public virtual DbSet<AccountModel> Accounts { get; set; } 
    }
}