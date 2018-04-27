﻿// <auto-generated />
using System;
using MaiDan.Billing.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MaiDan.Billing.Dal.Migrations
{
    [DbContext(typeof(BillingContext))]
    [Migration("20180427061614_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571");

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.Bill", b =>
                {
                    b.Property<int>("Id");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.BillTax", b =>
                {
                    b.Property<int>("BillId");

                    b.Property<int>("Index");

                    b.Property<decimal>("Amount");

                    b.Property<string>("TaxRateId")
                        .IsRequired();

                    b.HasKey("BillId", "Index");

                    b.HasIndex("TaxRateId");

                    b.ToTable("BillTax");
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.Dish", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("BillDish");
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.Line", b =>
                {
                    b.Property<int>("BillId");

                    b.Property<int>("Index");

                    b.Property<decimal>("Amount");

                    b.Property<decimal>("TaxAmount");

                    b.Property<string>("TaxRateId")
                        .IsRequired();

                    b.HasKey("BillId", "Index");

                    b.HasIndex("TaxRateId");

                    b.ToTable("BillLine");
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.Price", b =>
                {
                    b.Property<string>("DishId");

                    b.Property<DateTime>("ValidityStartDate");

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("ValidityEndDate");

                    b.HasKey("DishId", "ValidityStartDate");

                    b.ToTable("DishPrice");
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.TaxRate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Rate");

                    b.Property<string>("TaxId")
                        .IsRequired();

                    b.Property<DateTime>("ValidityEndDate");

                    b.Property<DateTime>("ValidityStartDate");

                    b.HasKey("Id");

                    b.ToTable("TaxRate");
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.BillTax", b =>
                {
                    b.HasOne("MaiDan.Billing.Dal.Entities.Bill")
                        .WithMany("Taxes")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MaiDan.Billing.Dal.Entities.TaxRate", "TaxRate")
                        .WithMany()
                        .HasForeignKey("TaxRateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.Line", b =>
                {
                    b.HasOne("MaiDan.Billing.Dal.Entities.Bill")
                        .WithMany("Lines")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MaiDan.Billing.Dal.Entities.TaxRate", "TaxRate")
                        .WithMany()
                        .HasForeignKey("TaxRateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaiDan.Billing.Dal.Entities.Price", b =>
                {
                    b.HasOne("MaiDan.Billing.Dal.Entities.Dish")
                        .WithMany("Prices")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
