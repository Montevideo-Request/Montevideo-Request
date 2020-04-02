using Microsoft.EntityFrameworkCore;
using IMMRequest.Domain;

namespace IMMRequest.DataAccess
{
    public partial class IMMRequestContext : DbContext
    {
        public DbSet<AdditionalField> AdditionalFields { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Type> Types { get; set; }

        public IMMRequestContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalField>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Request>().HasKey(x => x.Id);

            modelBuilder.Entity<Request>().HasOne(x => x.Area);
            modelBuilder.Entity<Request>().HasOne(x => x.Topic);
            modelBuilder.Entity<Request>().HasOne(x => x.Type);

            //Area & Topic Relation
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("area_id_IDX");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasMany(p => p.Topics)
                    .WithOne(d => d.Area)
                    .HasForeignKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("topics_id_area_fkey");
            });

            //Topic & Type Relation
            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("topic_id_IDX");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasMany(p => p.Types)
                    .WithOne(d => d.Topic)
                    .HasForeignKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("types_id_topic_fkey");
            });

            //Type & AdditionalField Relation
             modelBuilder.Entity<Type>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("type_id_IDX");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasMany(p => p.AdditionalFields)
                    .WithOne(d => d.Type)
                    .HasForeignKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("additionalfields_id_type_fkey");
            });

            //AdditionalField & Range Relation
             modelBuilder.Entity<AdditionalField>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("additionalField_id_IDX");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasMany(p => p.Ranges)
                    .WithOne(d => d.AdditionalField)
                    .HasForeignKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ranges_id_additionalfield_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
