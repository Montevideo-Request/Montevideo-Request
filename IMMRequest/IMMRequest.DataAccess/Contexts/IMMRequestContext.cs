using Microsoft.EntityFrameworkCore;
using IMMRequest.Domain;

namespace IMMRequest.DataAccess
{
    public partial class IMMRequestContext : DbContext
    {
        public DbSet<AdditionalField> AdditionalFields { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<AdditionalFieldValue> AdditionalFieldValues { get; set; }

        public IMMRequestContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>().HasKey(x => x.Id);

            /* Area & Topic Relation */
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasMany(p => p.Topics)
                    .WithOne(d => d.Area)
                    .HasForeignKey(p => p.AreaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /* Topic & Type Relation */
            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasMany(p => p.Types)
                    .WithOne(d => d.Topic)
                    .HasForeignKey(p => p.TopicId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            /* Type & AdditionalField Relation 8 Request  */
            modelBuilder.Entity<TypeEntity>(entity =>
           {
               entity.HasMany(p => p.AdditionalFields)
                   .WithOne(d => d.Type)
                   .HasForeignKey(p => p.TypeId)
                   .OnDelete(DeleteBehavior.NoAction);
           });

            /* AdditionalField & Range Relation */
            modelBuilder.Entity<AdditionalField>(entity =>
           {
               entity.HasMany(p => p.Ranges)
                   .WithOne(d => d.AdditionalField)
                   .HasForeignKey(p => p.AdditionalFieldId)
                   .OnDelete(DeleteBehavior.Cascade);
           });

            /* Request & FieldRangeValue Relation */
            modelBuilder.Entity<Request>(entity => 
            {
                entity.HasMany(p => p.AdditionalFieldValues)
                .WithOne( x => x.Request)
                .HasForeignKey( p => p.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
