﻿// <auto-generated />
using System;
using Futures.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Futures.Persistence.Migrations
{
    [DbContext(typeof(FuturesDbContext))]
    partial class FuturesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Futures.Domain.Models.FutureContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Asset")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ContractType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("FutureContracts", (string)null);
                });

            modelBuilder.Entity("Futures.Domain.Models.PriceDifference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrentFuturesContractId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Difference")
                        .HasColumnType("numeric");

                    b.Property<string>("IntervalType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("NextFuturesContractId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("CurrentFuturesContractId");

                    b.HasIndex("NextFuturesContractId");

                    b.ToTable("PriceDifferences", (string)null);
                });

            modelBuilder.Entity("Futures.Domain.Models.PricePoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FutureContractId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("FutureContractId");

                    b.ToTable("PricePoints", (string)null);
                });

            modelBuilder.Entity("Futures.Domain.Models.PriceDifference", b =>
                {
                    b.HasOne("Futures.Domain.Models.FutureContract", "CurrentFutureContract")
                        .WithMany()
                        .HasForeignKey("CurrentFuturesContractId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Futures.Domain.Models.FutureContract", "NextFutureContract")
                        .WithMany()
                        .HasForeignKey("NextFuturesContractId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrentFutureContract");

                    b.Navigation("NextFutureContract");
                });

            modelBuilder.Entity("Futures.Domain.Models.PricePoint", b =>
                {
                    b.HasOne("Futures.Domain.Models.FutureContract", "FutureContract")
                        .WithMany()
                        .HasForeignKey("FutureContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FutureContract");
                });
#pragma warning restore 612, 618
        }
    }
}
