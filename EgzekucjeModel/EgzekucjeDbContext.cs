using InfoSystemFirebirdConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Egzekucje.NET
{
    [DbConfigurationType(typeof(InfoSystemDbConfiguration))]
    public class EgzekucjeDbContext : DbContext
    {
        public static SqlProviderServices EntitySqlServerHack => SqlProviderServices.Instance;

        public EgzekucjeDbContext() 
            : base(InfoSystemDbConfiguration.ReadConnectionString("EgzekucjeDbContext", "DbApp.config"))
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Database.Log = Console.Write;
            Database.SetInitializer<EgzekucjeDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // cutomize conventions
            // https://andrewlock.net/customising-asp-net-core-identity-ef-core-naming-conventions-for-postgresql/
        }

        public DbSet<Upomnienie> Upomnienia { get; set; }
        public DbSet<Zaleglosc> Zaleglosci { get; set; }
    }
}
