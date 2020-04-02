﻿// <auto-generated />
using System;
using IMMRequest.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMMRequest.DataAccess.Migrations
{
    [DbContext(typeof(IMMRequestContext))]
    partial class IMMRequestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMMRequest.Domain.AdditionalField", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("FieldType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("additionalField_id_IDX");

                    b.ToTable("AdditionalFields");
                });

            modelBuilder.Entity("IMMRequest.Domain.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("area_id_IDX");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("IMMRequest.Domain.FieldRange", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Range")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FieldRange");
                });

            modelBuilder.Entity("IMMRequest.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("IMMRequest.Domain.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("RequestorsEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestorsName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestorsPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("TopicId");

                    b.HasIndex("TypeId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("IMMRequest.Domain.Topic", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("topic_id_IDX");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("IMMRequest.Domain.Type", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .HasName("type_id_IDX");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("IMMRequest.Domain.Administrator", b =>
                {
                    b.HasBaseType("IMMRequest.Domain.Person");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("IMMRequest.Domain.AdditionalField", b =>
                {
                    b.HasOne("IMMRequest.Domain.Type", "Type")
                        .WithMany("AdditionalFields")
                        .HasForeignKey("Id")
                        .HasConstraintName("additionalfields_id_type_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMMRequest.Domain.FieldRange", b =>
                {
                    b.HasOne("IMMRequest.Domain.AdditionalField", "AdditionalField")
                        .WithMany("Ranges")
                        .HasForeignKey("Id")
                        .HasConstraintName("ranges_id_additionalfield_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMMRequest.Domain.Request", b =>
                {
                    b.HasOne("IMMRequest.Domain.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("IMMRequest.Domain.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("IMMRequest.Domain.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("IMMRequest.Domain.Topic", b =>
                {
                    b.HasOne("IMMRequest.Domain.Area", "Area")
                        .WithMany("Topics")
                        .HasForeignKey("Id")
                        .HasConstraintName("topics_id_area_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IMMRequest.Domain.Type", b =>
                {
                    b.HasOne("IMMRequest.Domain.Topic", "Topic")
                        .WithMany("Types")
                        .HasForeignKey("Id")
                        .HasConstraintName("types_id_topic_fkey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
