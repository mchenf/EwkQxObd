using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Views;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Data
{
    public class EwkIqxObdContext : DbContext
    {
        public EwkIqxObdContext(DbContextOptions<EwkIqxObdContext> options) : base(options)
        {
            
        }

        public DbSet<IqxSystem> IqxSystem { get; set; }
        public DbSet<IqxNetworkInstrument> IqxInstrument { get; set; }
        public DbSet<IqxOrganization> IqxOrganisation { get; set; }


        public DbSet<EqoContractObject> EqoContractObject { get; set; }
        public DbSet<EqoContract> EqoContract { get; set; }
        public DbSet<EqoContactInfo> EqoContactInfo { get; set; }
        public DbSet<EqoAccount> EqoAccount { get; set; }


        public DbSet<EqoTicketSource> EqoTicketSource { get; set; }

        public DbSet<vwSysnetinst> VwSysnetinst { get; set; }
        public DbSet<vwSysnetinstorg> VwSysnetinstorg { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vwSysnetinst>(ent => {
                ent.ToView("vw_LatestSysnetinst");
                ent.HasNoKey();
            });

            modelBuilder.Entity<vwSysnetinstorg>(ent => {
                ent.ToView("vw_LatestSysnetinstorg");
                ent.HasNoKey();
            });

        }
    }
}
