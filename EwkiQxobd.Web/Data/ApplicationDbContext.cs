using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EwkiQxobd.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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

        public DbSet<Syngi> VwSysnetinst { get; set; }
        public DbSet<Syngio> VwSysnetinstorg { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Syngi>(ent => {
                ent.ToView("vw_LatestSysnetinst");
                ent.HasNoKey();
            });

            modelBuilder.Entity<Syngio>(ent => {
                ent.ToView("vw_LatestSysnetinstorg");
                ent.HasNoKey();
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(ent =>
            {
                ent.HasNoKey();
            });
            modelBuilder.Entity<IdentityUserRole<string>>(ent =>
            {
                ent.HasNoKey();
            });

            modelBuilder.Entity<IdentityUserToken<string>>(ent =>
            {
                ent.HasNoKey();
            });

        }
    }
}
