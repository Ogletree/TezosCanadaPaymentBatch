using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TezosService.Model;
using TezosService.Model.Mine;

namespace TezosService.Connectors
{
    class MyContext : DbContext
    {
        public DbSet<DelegateConfig> DelegateConfig { get; set; }
        public DbSet<DelegatePayments> DelegatePayments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}