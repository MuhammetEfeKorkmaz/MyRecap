using Microsoft.EntityFrameworkCore;
using Recap.Core.Entities.Concrete;
using Recap.Entities.Concrete;

namespace Erp.Dal.Concrete.Factories.EntityFramework
{
    public class ReCapContext : DbContext
    {
        public ReCapContext(DbContextOptions<ReCapContext> options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public ReCapContext()
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PROJECTERP;User ID=sa;Password=Sadsa_123;Persist Security Info=True;");
        }

        public DbSet<Departman> Departman { get; set; }

        public DbSet<Unvan> Unvan { get; set; } 
        public DbSet<User> User { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaim { get; set; }
        public DbSet<OperationClaim> OperationClaim { get; set; }

      

      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<User>(ent =>
            {
                ent.Property(x => x.Id).UseIdentityColumn().IsRequired();
                ent.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(1).IsRequired();             
                ent.Property(x => x.Email).HasDefaultValue("Girilmemiş").HasMaxLength(4000).IsRequired();
            });
            modelBuilder.Entity<Departman>(ent =>
            {
                ent.Property(x => x.Id).UseIdentityColumn().IsRequired();
                ent.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(1).IsRequired();
                ent.Property(x => x.Name).HasDefaultValue("Girilmemiş").HasMaxLength(4000).IsRequired();
                ent.Property(x => x.RootPath).HasDefaultValue("Girilmemiş").HasMaxLength(4000).IsRequired();
                ent.Property(x => x.WinUserName).HasDefaultValue("Girilmemiş").HasMaxLength(4000).IsRequired();
                ent.Property(x => x.WinPassword).HasDefaultValue("Girilmemiş").HasMaxLength(4000).IsRequired();

            });
            modelBuilder.Entity<Unvan>(ent =>
            {
                ent.Property(x => x.Id).UseIdentityColumn().IsRequired();
                ent.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(1).IsRequired();
                ent.Property(x => x.Name).HasDefaultValue("Girilmemiş").HasMaxLength(4000).IsRequired();


            });

        


            modelBuilder.Entity<UserOperationClaim>(ent =>
            {
                ent.Property(x => x.Id).UseIdentityColumn().IsRequired();
                ent.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(1).IsRequired();

 
            });
            modelBuilder.Entity<OperationClaim>(ent =>
            {
                ent.Property(x => x.Id).UseIdentityColumn().IsRequired();
                ent.Property(x => x.IsActive).HasColumnType("bit").HasDefaultValue(1).IsRequired();

 
            });

        }
    }
}
