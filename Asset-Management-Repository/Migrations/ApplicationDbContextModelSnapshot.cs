﻿// <auto-generated />
using System;
using Asset_Management.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Asset_Management_Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Asset_Management.Repository.AccountEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("address");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("full_name");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("account");
                });

            modelBuilder.Entity("Asset_Management.Repository.ApprovalEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("reason");

                    b.Property<long>("RequestAssetId")
                        .HasColumnType("bigint")
                        .HasColumnName("request_asset_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("RequestAssetId");

                    b.ToTable("approval");
                });

            modelBuilder.Entity("Asset_Management.Repository.AssetEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("asset_name");

                    b.Property<int>("PurchaseYear")
                        .HasColumnType("int")
                        .HasColumnName("purchase_year");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("serial_number");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("specification");

                    b.HasKey("Id");

                    b.ToTable("asset");
                });

            modelBuilder.Entity("Asset_Management.Repository.RequestAssetEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("PicId")
                        .HasColumnType("bigint")
                        .HasColumnName("pic_id");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("request_date");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("specification");

                    b.HasKey("Id");

                    b.HasIndex("PicId");

                    b.ToTable("request_asset");
                });

            modelBuilder.Entity("Asset_Management.Repository.ApprovalEntity", b =>
                {
                    b.HasOne("Asset_Management.Repository.RequestAssetEntity", "RequestAsset")
                        .WithMany("approval")
                        .HasForeignKey("RequestAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestAsset");
                });

            modelBuilder.Entity("Asset_Management.Repository.RequestAssetEntity", b =>
                {
                    b.HasOne("Asset_Management.Repository.AccountEntity", "Pic")
                        .WithMany()
                        .HasForeignKey("PicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pic");
                });

            modelBuilder.Entity("Asset_Management.Repository.RequestAssetEntity", b =>
                {
                    b.Navigation("approval");
                });
#pragma warning restore 612, 618
        }
    }
}
