using Microsoft.EntityFrameworkCore;
using IMMRequest.Domain;
using System;

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
                   .OnDelete(DeleteBehavior.Cascade);
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
                .OnDelete(DeleteBehavior.NoAction);
            });

            #region Super Admin
                Guid baseToken = new Guid();
                Administrator admin = new Administrator() { Name = "Administrator", Email = "main@admin.com", Password = "root", Token = baseToken };
            #endregion


            #region Default Areas
                Area espaciosPublicosCalles = new Area() { Name = "Espacios Públicos y Calles" };
                Area limpieza = new Area() { Name = "Limpieza" };
                Area saneamiento = new Area() { Name = "Saneamiento" };
                Area transporte = new Area() { Name = "Transporte" };    
            #endregion


            #region Default Topics
            
                /* Espacios Públicos y Calles */
                Topic alumbrado = new Topic() { AreaId = espaciosPublicosCalles.Id, Name = "Alumbrado"};
                Topic arboladoPlantacion = new Topic() { AreaId = espaciosPublicosCalles.Id, Name = "Arbolado y plantación"};
                Topic equipamientoUrbano = new Topic() { AreaId = espaciosPublicosCalles.Id, Name = "Equipamiento urbano"};
                Topic fuentesGraffitisInstalaciones = new Topic() { AreaId = espaciosPublicosCalles.Id, Name = "Fuentes, graffitis e instalaciones"}; 
                Topic otrosPublicoCalles = new Topic() { AreaId = espaciosPublicosCalles.Id, Name = "Otros"}; 

                /* Limpieza */
                Topic estadoContenedores = new Topic() { AreaId = limpieza.Id, Name = "Estado de los contenedores"};
                Topic problemasLimpieza = new Topic() { AreaId = limpieza.Id, Name = "Problemas de limpieza"};
                Topic solicitudRetiro = new Topic() { AreaId = limpieza.Id, Name = "Solicitud de retiro de poda, escombros o residuos de gran tamaño"};
                Topic otrosLimpieza = new Topic() { AreaId = limpieza.Id, Name = "Otros"}; 

                /* Saneamiento */
                Topic bocasDeTormenta = new Topic() { AreaId = saneamiento.Id, Name = "Bocas de tormenta"};
                Topic obstruccionesPerdidas = new Topic() { AreaId = transporte.Id, Name = "Obstrucciones o pérdidas"};
                Topic otrosSaneamiento = new Topic() { AreaId = saneamiento.Id, Name = "Otros"}; 
                
                /* Transporte */
                Topic ambulancias = new Topic() { AreaId = transporte.Id, Name = "Ambulancias"};
                Topic terminales = new Topic() { AreaId = transporte.Id, Name = "Terminales"};
                Topic acosoSexual = new Topic() { AreaId = transporte.Id, Name = "Acoso sexual"};
                Topic paradas = new Topic() { AreaId = transporte.Id, Name = "Paradas"};
                Topic taxisRemisesEscolares = new Topic() { AreaId = transporte.Id, Name = "Taxis, remises, escolares"};
                Topic transporteColectivo = new Topic() { AreaId = transporte.Id, Name = "Transporte colectivo"};
                Topic otrosTransporte = new Topic() { AreaId = transporte.Id, Name = "Otros"};                
            #endregion

            
            /* Seeding DataBase */
            modelBuilder.Entity<Administrator>().HasData( admin );
            modelBuilder.Entity<Area>().HasData( espaciosPublicosCalles, limpieza,  saneamiento, transporte );
            modelBuilder.Entity<Topic>().HasData
            (
                alumbrado, arboladoPlantacion, equipamientoUrbano, fuentesGraffitisInstalaciones, otrosPublicoCalles,
                estadoContenedores, problemasLimpieza, solicitudRetiro, otrosLimpieza, 
                bocasDeTormenta, obstruccionesPerdidas, otrosSaneamiento, 
                ambulancias, terminales, acosoSexual, paradas, taxisRemisesEscolares, transporteColectivo, otrosTransporte
            );

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
