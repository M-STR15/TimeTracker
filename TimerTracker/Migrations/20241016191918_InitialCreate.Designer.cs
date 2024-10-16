﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimerTracker.DataAccess;

#nullable disable

namespace TimerTracker.Migrations
{
    [DbContext(typeof(MainDatacontext))]
    [Migration("20241016191918_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("TimerTracker.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Activity_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Activities", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Start"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pause"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Stop"
                        });
                });

            modelBuilder.Entity("TimerTracker.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Project_ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Project", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "",
                            Name = "Project 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            Name = "Project 2"
                        });
                });

            modelBuilder.Entity("TimerTracker.Models.RecordActivity", b =>
                {
                    b.Property<Guid>("GuidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Guid_ID");

                    b.Property<int>("ActivityId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Activity_ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Project_ID");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("Start_time");

                    b.HasKey("GuidId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Record_activity", "dbo");
                });

            modelBuilder.Entity("TimerTracker.Models.RecordActivity", b =>
                {
                    b.HasOne("TimerTracker.Models.Activity", "Activity")
                        .WithMany("Activities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimerTracker.Models.Project", "Project")
                        .WithMany("Activities")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TimerTracker.Models.Activity", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("TimerTracker.Models.Project", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
