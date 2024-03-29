﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleFleetManager.Models.EntityFramework;

#nullable disable

namespace SimpleFleetManager.Migrations
{
    [DbContext(typeof(SimpleDbContext))]
    [Migration("20240329090009_290320240002")]
    partial class _290320240002
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("SimpleFleetManager.Models.Common.AMR.Misc.Config.TebConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AllowInitWithBackwardMotion")
                        .HasColumnType("INTEGER");

                    b.Property<double>("DtHysteresis")
                        .HasColumnType("REAL");

                    b.Property<double>("DtRef")
                        .HasColumnType("REAL");

                    b.Property<double>("DynamicObstacleInflationRadius")
                        .HasColumnType("REAL");

                    b.Property<int>("ForkliftId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("GoalAngularTolerance")
                        .HasColumnType("REAL");

                    b.Property<double>("GoalLinearTolerance")
                        .HasColumnType("REAL");

                    b.Property<bool>("IncludeCostmapObstacles")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IncludeDynamicObstacles")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MaxAccAngular")
                        .HasColumnType("REAL");

                    b.Property<double>("MaxAccForwardBackward")
                        .HasColumnType("REAL");

                    b.Property<double>("MaxVelAngular")
                        .HasColumnType("REAL");

                    b.Property<double>("MaxVelBackward")
                        .HasColumnType("REAL");

                    b.Property<double>("MaxVelForward")
                        .HasColumnType("REAL");

                    b.Property<double>("MinObstacleDistance")
                        .HasColumnType("REAL");

                    b.Property<double>("ObstacleInflationRadius")
                        .HasColumnType("REAL");

                    b.Property<bool>("OscillationRecovery")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Save")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TurningRadius")
                        .HasColumnType("REAL");

                    b.Property<double>("Wheelbase")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ForkliftId")
                        .IsUnique();

                    b.ToTable("TebConfigs");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Common.AMR.Misc.ForkliftLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CodeLine")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("File")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForkliftId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("Node")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.Forklift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForkliftIpAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LidarLocIpAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Registrationdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("VisionaryIpAddress")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Forklifts");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentJobStep")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsQueued")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.JobStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("LocR")
                        .HasColumnType("REAL");

                    b.Property<double>("LocX")
                        .HasColumnType("REAL");

                    b.Property<double>("LocY")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobSteps");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("R")
                        .HasColumnType("REAL");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<double>("X")
                        .HasColumnType("REAL");

                    b.Property<double>("Y")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardTag")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLogged")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Common.AMR.Misc.Config.TebConfig", b =>
                {
                    b.HasOne("SimpleFleetManager.Models.Main.Forklift", null)
                        .WithOne("BackedUpTebConfig")
                        .HasForeignKey("SimpleFleetManager.Models.Common.AMR.Misc.Config.TebConfig", "ForkliftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.JobStep", b =>
                {
                    b.HasOne("SimpleFleetManager.Models.Main.Job", null)
                        .WithMany("JobSteps")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.Forklift", b =>
                {
                    b.Navigation("BackedUpTebConfig");
                });

            modelBuilder.Entity("SimpleFleetManager.Models.Main.Job", b =>
                {
                    b.Navigation("JobSteps");
                });
#pragma warning restore 612, 618
        }
    }
}
