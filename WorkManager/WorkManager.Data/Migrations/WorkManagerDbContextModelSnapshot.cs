﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkManager.Data.Contexts;

#nullable disable

namespace WorkManager.Data.Migrations
{
    [DbContext(typeof(WorkManagerDbContext))]
    partial class WorkManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkManager.Data.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WorkManager.Data.Models.ClientContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<long>("TimeSpanTicks")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("ClientContracts");
                });

            modelBuilder.Entity("WorkManager.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("HourSalary")
                        .HasColumnType("money");

                    b.Property<int?>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(128)");

                    b.Property<long>("TimeSpanTicks")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WorkManager.Data.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<long>("TimeSpanTicks")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("WorkManager.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WorkManager.Data.Models.ClientContract", b =>
                {
                    b.HasOne("WorkManager.Data.Models.Invoice", null)
                        .WithMany("CurrentContractIds")
                        .HasForeignKey("InvoiceId");
                });

            modelBuilder.Entity("WorkManager.Data.Models.Invoice", b =>
                {
                    b.Navigation("CurrentContractIds");
                });
#pragma warning restore 612, 618
        }
    }
}
