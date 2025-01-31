﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartLightSense;

#nullable disable

namespace SmartLightSense.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20250108123401_Add_Message_Field_To_Alerts")]
    partial class Add_Message_Field_To_Alerts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartLightSense.Models.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AlertDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("AlertType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Message")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("Resolved")
                        .HasColumnType("bit");

                    b.Property<int?>("SensorId")
                        .HasColumnType("int");

                    b.Property<int?>("StreetlightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.HasIndex("StreetlightId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("SmartLightSense.Models.EnergyUsage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("EnergyConsumed")
                        .HasColumnType("float");

                    b.Property<int>("StreetlightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StreetlightId");

                    b.ToTable("EnergyUsages");
                });

            modelBuilder.Entity("SmartLightSense.Models.MaintenanceLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionTaken")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("AlertId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssueReported")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("StreetlightId")
                        .HasColumnType("int");

                    b.Property<int>("TechnicianId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlertId");

                    b.HasIndex("StreetlightId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("MaintenanceLogs");
                });

            modelBuilder.Entity("SmartLightSense.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Data")
                        .HasColumnType("int");

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SensorType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StreetlightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StreetlightId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("SmartLightSense.Models.Streetlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrightnessLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastMaintenanceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SectorId")
                        .HasMaxLength(255)
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Streetlights");
                });

            modelBuilder.Entity("SmartLightSense.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartLightSense.Models.WeatherData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Visibility")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WeatherData");
                });

            modelBuilder.Entity("SmartLightSense.Models.Alert", b =>
                {
                    b.HasOne("SmartLightSense.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SmartLightSense.Models.Streetlight", "Streetlight")
                        .WithMany()
                        .HasForeignKey("StreetlightId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Sensor");

                    b.Navigation("Streetlight");
                });

            modelBuilder.Entity("SmartLightSense.Models.EnergyUsage", b =>
                {
                    b.HasOne("SmartLightSense.Models.Streetlight", "Streetlight")
                        .WithMany()
                        .HasForeignKey("StreetlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Streetlight");
                });

            modelBuilder.Entity("SmartLightSense.Models.MaintenanceLog", b =>
                {
                    b.HasOne("SmartLightSense.Models.Alert", "Alert")
                        .WithMany()
                        .HasForeignKey("AlertId");

                    b.HasOne("SmartLightSense.Models.Streetlight", "Streetlight")
                        .WithMany()
                        .HasForeignKey("StreetlightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartLightSense.Models.User", "Technician")
                        .WithMany()
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Alert");

                    b.Navigation("Streetlight");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("SmartLightSense.Models.Sensor", b =>
                {
                    b.HasOne("SmartLightSense.Models.Streetlight", "Streetlight")
                        .WithMany("Sensors")
                        .HasForeignKey("StreetlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Streetlight");
                });

            modelBuilder.Entity("SmartLightSense.Models.Streetlight", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
