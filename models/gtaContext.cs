using nussigesfreeroam.Utils;
using Microsoft.EntityFrameworkCore;
using AltV.Net;


namespace nussigesfreeroam.models
{
    public partial class gtaContext : DbContext
    {
        public gtaContext() { }
        public gtaContext(DbContextOptions<gtaContext> options) : base(options) { }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Accounts_Chardata> Accounts_Chardata { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if(!optionsBuilder.IsConfigured)
                {
                    string connectionStr = "";
                    //Lokal
                    if (!Alt.IsDebug)
                    {
                        connectionStr = $"server={Constants.DatabaseConfig.Host};port={Constants.DatabaseConfig.Port};user={Constants.DatabaseConfig.User};password={Constants.DatabaseConfig.Password};database={Constants.DatabaseConfig.Database}";
                    }
                    else if(Alt.IsDebug)
                    {
                        connectionStr = $"server={Constants.DatabaseConfig.TestHost};port={Constants.DatabaseConfig.TestPort};user={Constants.DatabaseConfig.TestUser};password={Constants.DatabaseConfig.TestPassword};database={Constants.DatabaseConfig.TestDatabase}";
                    }
                    optionsBuilder.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr));
                    optionsBuilder.EnableSensitiveDataLogging();
                }
            }
            catch (Exception e)
            {
                Alt.Log($"pupsi{e}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

                modelBuilder.Entity<Accounts>(entity =>
                {
                    entity.HasKey(e => e.id);
                    entity.ToTable("accounts", Constants.DatabaseConfig.Database);
                    entity.HasIndex(e => e.id).HasDatabaseName("id");
                    entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                    entity.Property(e => e.discordId).HasColumnName("discordId");
                    entity.Property(e => e.socialClub).HasColumnName("socialClubId");
                    entity.Property(e => e.name).HasColumnName("name");
                    entity.Property(e => e.isBanned).HasColumnName("banned");
                    entity.Property(e => e.adminLevel).HasColumnName("adminlevel");
                    entity.Property(e => e.regDate).HasColumnName("regDate");
                    entity.Property(e => e.hwid).HasColumnName("hwid");
                    entity.Property(e => e.kills).HasColumnName("kills");
                    entity.Property(e => e.deaths).HasColumnName("deaths");
                    entity.Property(e => e.level).HasColumnName("level");
                    entity.Property(e => e.money).HasColumnName("money");

                });

                modelBuilder.Entity<Accounts_Chardata>(entity =>
                {
                    entity.HasKey(e => e.playerid);
                    entity.ToTable("accounts_chardata", Constants.DatabaseConfig.Database);
                    entity.HasIndex(e => e.playerid).HasDatabaseName("playerid");
                    entity.Property(e => e.playerid).HasColumnName("playerid").HasColumnType("int(11)");
                    entity.Property(e => e.charData).HasColumnName("charData");
                });

                modelBuilder.Entity<Vehicles>(entity =>
                {
                    entity.HasKey(e => e.id);
                    entity.ToTable("vehicles", Constants.DatabaseConfig.Database);
                    entity.HasIndex(e => e.id).HasDatabaseName("id");
                    entity.Property(e => e.id).HasColumnName("id").HasColumnType("int(11)");
                    entity.Property(e => e.spawnHash).HasColumnName("spawnHash");
                    entity.Property(e => e.spawnName).HasColumnName("spawnName");
                    entity.Property(e => e.lastPosX).HasColumnName("lastPosX");
                    entity.Property(e => e.lastPosY).HasColumnName("lastPosY");
                    entity.Property(e => e.lastPosZ).HasColumnName("lastPosZ");
                    entity.Property(e => e.lastRotX).HasColumnName("lastRotX");
                    entity.Property(e => e.lastRotY).HasColumnName("lastRotY");
                    entity.Property(e => e.lastRotZ).HasColumnName("lastRotZ");
                    entity.Property(e => e.created).HasColumnName("created");
                });
            }
            catch (Exception e)
            {
                Alt.Log($"blabla{e}");
            }
        }
    }
}