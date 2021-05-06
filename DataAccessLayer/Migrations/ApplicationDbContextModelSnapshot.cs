﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Entity.Dragon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date_Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dragons");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date_Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gun")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Hit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DragonId")
                        .HasColumnType("int");

                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.Property<int>("ImpactForce")
                        .HasColumnType("int");

                    b.Property<DateTime>("ImpactTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DragonId");

                    b.HasIndex("HeroId");

                    b.ToTable("Hits");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Hit", b =>
                {
                    b.HasOne("DataAccessLayer.Entity.Dragon", "Dragons")
                        .WithMany("Hits")
                        .HasForeignKey("DragonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entity.Hero", "Heros")
                        .WithMany("Hits")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dragons");

                    b.Navigation("Heros");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Dragon", b =>
                {
                    b.Navigation("Hits");
                });

            modelBuilder.Entity("DataAccessLayer.Entity.Hero", b =>
                {
                    b.Navigation("Hits");
                });
#pragma warning restore 612, 618
        }
    }
}
