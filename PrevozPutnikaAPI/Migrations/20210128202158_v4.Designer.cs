﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrevozPutnikaAPI.Models;

namespace PrevozPutnikaAPI.Migrations
{
    [DbContext(typeof(PrevozContext))]
    [Migration("20210128202158_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("punoIme")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("punoIme");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Relacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("brojSedista")
                        .HasColumnType("int")
                        .HasColumnName("brojSedista");

                    b.Property<string>("izlaz")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("izlaz");

                    b.Property<string>("ulaz")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ulaz");

                    b.Property<DateTime>("vremePolaska")
                        .HasColumnType("datetime2")
                        .HasColumnName("vremePolaska");

                    b.HasKey("ID");

                    b.ToTable("Relacija");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Rezervacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int?>("korisnikID")
                        .HasColumnType("int");

                    b.Property<int?>("relacijaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("korisnikID");

                    b.HasIndex("relacijaID");

                    b.ToTable("Rezervacija");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Sediste", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<int>("brojSedista")
                        .HasColumnType("int")
                        .HasColumnName("brojSedista");

                    b.Property<int?>("relacijaID")
                        .HasColumnType("int");

                    b.Property<int?>("rezervacijaID")
                        .HasColumnType("int");

                    b.Property<bool>("zauzeto")
                        .HasColumnType("bit")
                        .HasColumnName("zauzeto");

                    b.HasKey("ID");

                    b.HasIndex("relacijaID");

                    b.HasIndex("rezervacijaID");

                    b.ToTable("Sediste");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Rezervacija", b =>
                {
                    b.HasOne("PrevozPutnikaAPI.Models.Korisnik", "korisnik")
                        .WithMany("rezervacije")
                        .HasForeignKey("korisnikID");

                    b.HasOne("PrevozPutnikaAPI.Models.Relacija", "relacija")
                        .WithMany()
                        .HasForeignKey("relacijaID");

                    b.Navigation("korisnik");

                    b.Navigation("relacija");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Sediste", b =>
                {
                    b.HasOne("PrevozPutnikaAPI.Models.Relacija", "relacija")
                        .WithMany("skupSedista")
                        .HasForeignKey("relacijaID");

                    b.HasOne("PrevozPutnikaAPI.Models.Rezervacija", "rezervacija")
                        .WithMany("sedista")
                        .HasForeignKey("rezervacijaID");

                    b.Navigation("relacija");

                    b.Navigation("rezervacija");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Korisnik", b =>
                {
                    b.Navigation("rezervacije");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Relacija", b =>
                {
                    b.Navigation("skupSedista");
                });

            modelBuilder.Entity("PrevozPutnikaAPI.Models.Rezervacija", b =>
                {
                    b.Navigation("sedista");
                });
#pragma warning restore 612, 618
        }
    }
}
