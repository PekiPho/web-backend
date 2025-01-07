﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTemplate.Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(IspitContext))]
    partial class IspitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebTemplate.Models.Biblioteka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Biblioteka");
                });

            modelBuilder.Entity("WebTemplate.Models.Izdavanje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BibliotekaId")
                        .HasColumnType("int");

                    b.Property<int?>("KnjigaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VremeIzdavanja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VremeVracanja")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BibliotekaId");

                    b.HasIndex("KnjigaId");

                    b.ToTable("Izdavanje");
                });

            modelBuilder.Entity("WebTemplate.Models.Knjiga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BibliotekaId")
                        .HasColumnType("int");

                    b.Property<long>("BrojUEvidenciji")
                        .HasColumnType("bigint");

                    b.Property<long>("GodinaIzdavanja")
                        .HasColumnType("bigint");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NazivIzdavaca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BibliotekaId");

                    b.ToTable("Knjiga");
                });

            modelBuilder.Entity("WebTemplate.Models.Izdavanje", b =>
                {
                    b.HasOne("WebTemplate.Models.Biblioteka", "Biblioteka")
                        .WithMany()
                        .HasForeignKey("BibliotekaId");

                    b.HasOne("WebTemplate.Models.Knjiga", "Knjiga")
                        .WithMany("Izdata")
                        .HasForeignKey("KnjigaId");

                    b.Navigation("Biblioteka");

                    b.Navigation("Knjiga");
                });

            modelBuilder.Entity("WebTemplate.Models.Knjiga", b =>
                {
                    b.HasOne("WebTemplate.Models.Biblioteka", "Biblioteka")
                        .WithMany("Izdate")
                        .HasForeignKey("BibliotekaId");

                    b.Navigation("Biblioteka");
                });

            modelBuilder.Entity("WebTemplate.Models.Biblioteka", b =>
                {
                    b.Navigation("Izdate");
                });

            modelBuilder.Entity("WebTemplate.Models.Knjiga", b =>
                {
                    b.Navigation("Izdata");
                });
#pragma warning restore 612, 618
        }
    }
}
