﻿// <auto-generated />
using System;
using BloodAppTry.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloodAppTry.Migrations
{
    [DbContext(typeof(BloodContext))]
    [Migration("20230321204542_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BloodAppTry.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("BloodAppTry.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DonationCenterID")
                        .HasColumnType("int");

                    b.Property<int>("DonorID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonationCenterID");

                    b.HasIndex("DonorID");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("BloodAppTry.Models.BloodBank", b =>
                {
                    b.Property<int>("BloodBankID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BloodBankID"));

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BloodBankID");

                    b.ToTable("BloodBank", (string)null);
                });

            modelBuilder.Entity("BloodAppTry.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorID"));

                    b.Property<string>("CNP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DonationCenterID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorID");

                    b.HasIndex("DonationCenterID");

                    b.ToTable("Doctor", (string)null);
                });

            modelBuilder.Entity("BloodAppTry.Models.DonationCenter", b =>
                {
                    b.Property<int>("DonationCenterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonationCenterID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BloodBankID")
                        .HasColumnType("int");

                    b.Property<int>("ClosesAt")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("OpensAt")
                        .HasColumnType("int");

                    b.HasKey("DonationCenterID");

                    b.HasIndex("BloodBankID");

                    b.ToTable("DonationCenter", (string)null);
                });

            modelBuilder.Entity("BloodAppTry.Models.Donor", b =>
                {
                    b.Property<int>("DonorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonorID"));

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DonorID");

                    b.ToTable("Donor", (string)null);
                });

            modelBuilder.Entity("BloodAppTry.Models.Appointment", b =>
                {
                    b.HasOne("BloodAppTry.Models.DonationCenter", "DonationCenter")
                        .WithMany("Appointments")
                        .HasForeignKey("DonationCenterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloodAppTry.Models.Donor", "Donor")
                        .WithMany("Appointments")
                        .HasForeignKey("DonorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonationCenter");

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("BloodAppTry.Models.Doctor", b =>
                {
                    b.HasOne("BloodAppTry.Models.DonationCenter", "DonationCenter")
                        .WithMany("Doctors")
                        .HasForeignKey("DonationCenterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonationCenter");
                });

            modelBuilder.Entity("BloodAppTry.Models.DonationCenter", b =>
                {
                    b.HasOne("BloodAppTry.Models.BloodBank", "BloodBank")
                        .WithMany("donationCenters")
                        .HasForeignKey("BloodBankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BloodBank");
                });

            modelBuilder.Entity("BloodAppTry.Models.BloodBank", b =>
                {
                    b.Navigation("donationCenters");
                });

            modelBuilder.Entity("BloodAppTry.Models.DonationCenter", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("BloodAppTry.Models.Donor", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}