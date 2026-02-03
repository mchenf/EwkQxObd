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
        [Obsolete("Use IQX Organization")]
        public DbSet<EqoAccount> EqoAccount { get; set; }


        public DbSet<EqoTicketSource> EqoTicketSource { get; set; }

        public DbSet<Syngi> Syngi { get; set; }
        public DbSet<Syngio> Syngio { get; set; }

        public DbSet<RelTicketSourceContract> RelTicketSourceContracts { get; set; }

        public DbSet<FscEnterpriseInstance> FscEnterpriseInstance { get; set; }
        public DbSet<InstrumentLinkStatus> InstrumentLinkStatus { get; set; }
        public DbSet<Vorlks> Vorlks { get; set; }

        public DbSet<SyngioViewSystem> SyngioViewSystems { get; set; }
        public DbSet<SyngioViewNetwork> SyngioViewNetworks { get; set; }

        public DbSet<SyngioSearchAlpha> SyngioSearchAlpha { get; set; }

        public DbSet<EqoTaskWorkflow> TaskWorkFlow { get; set; }
        public DbSet<EqoTask> Task { get; set; }
        public DbSet<EqoTaskItem> TaskItem { get; set; }

        public DbSet<EwkxInstrumentType> InstrumentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var rel in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                rel.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }


            modelBuilder.Entity<Syngi>(ent => {
                ent.ToView("vw_LatestSysnetinst");
                ent.HasNoKey();
            });

            modelBuilder.Entity<Syngio>(ent => {
                ent.ToView("vw_LatestSysnetinstorg");
                ent.HasNoKey();
            });

            modelBuilder.Entity<Vorlks>(ent => {
                ent.ToView("vw_OrganizationLinkStatus");
                ent.HasNoKey();
            });

            modelBuilder.Entity<InstrumentLinkStatus>(ent => {
                ent.ToView("vwInstrumentLinkStatus");
                ent.HasNoKey();
            });

            modelBuilder.Entity<SyngioViewSystem>(ent =>
            {
                ent.ToView("vw_Syngio_Index_viewmodel");
                ent.HasNoKey();
            });

            modelBuilder.Entity<SyngioViewNetwork>(ent =>
            {
                ent.ToView("vw_Syngio_Network_Index_viewmodel");
                ent.HasNoKey();
            });

            modelBuilder.Entity<SyngioSearchAlpha>(ent =>
            {
                ent.ToView("vw_Syngio_Search_Alpha");
                ent.HasNoKey();
            });

            modelBuilder.Entity<EqoContractObject>()
                .HasIndex(eco =>
                new { eco.ContractId, eco.SerialNumber }
            ).IsUnique();

            modelBuilder.Entity<IqxOrganization>()
                .ToTable(tb => tb.HasTrigger("trg_SyncAccountGuid"));

        }
    }
}
