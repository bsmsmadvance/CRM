﻿// <auto-generated />
using System;
using Database.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.AuditLogs.Migrations
{
    [DbContext(typeof(AuditLogContext))]
    partial class AuditLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.AuditLogs.PRJ.UnitLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("UnitLog","PRJ");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterBookingCreditCardItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterBookingCreditCardItemLog","PRM");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterBookingPromotionFreeItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterBookingPromotionFreeItemLogs","PRM");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterBookingPromotionItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterBookingPromotionItemLogs","PRM");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterPreSalePromotionItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterPreSalePromotionItemLogs","PRM");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterTransferCreditCardItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterTransferCreditCardItemLogs","PRM");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterTransferPromotionFreeItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterTransferPromotionFreeItemLogs","PRM");
                });

            modelBuilder.Entity("Database.AuditLogs.PRM.MasterTransferPromotionItemLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Created");

                    b.Property<Guid>("KeyValue");

                    b.Property<string>("NewValues");

                    b.Property<string>("OldValues");

                    b.HasKey("ID");

                    b.ToTable("MasterTransferPromotionItemLogs","PRM");
                });
#pragma warning restore 612, 618
        }
    }
}
