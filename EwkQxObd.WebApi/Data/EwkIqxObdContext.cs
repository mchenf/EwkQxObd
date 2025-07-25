using EwkQxObd.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Data
{
    public class EwkIqxObdContext : DbContext
    {
        public EwkIqxObdContext(DbContextOptions<EwkIqxObdContext> options) : base(options)
        {
            
        }

        public DbSet<IqxSystem> IqxSystems { get; set; }
        public DbSet<IqxNetwork> IqxNetworks { get; set; }
        public DbSet<IqxOrganization> IqxOrganisation { get; set; }


        public DbSet<EqoContractObject> EqoContractObject { get; set; }
        public DbSet<EqoContract> EqoContract { get; set; }
        public DbSet<EqoContactInfo> EqoContactInfo { get; set; }
        public DbSet<EqoAccount> EqoAccount { get; set; }


    }
}
