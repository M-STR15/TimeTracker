﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimerTracker.DataAccess;

#nullable disable

namespace TimerTracker.Migrations
{
    [DbContext(typeof(MainDatacontext))]
    partial class MainDatacontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Project_ID");

                    b.Property<Guid?>("ShiftGuidId")
                        .HasColumnType("TEXT")
                        .HasColumnName("Shift_GuidId");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("Start_time");

                    b.HasKey("GuidId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ShiftGuidId");

                    b.ToTable("Record_activity", "dbo");
                });

            modelBuilder.Entity("TimerTracker.Models.Shift", b =>
                {
                    b.Property<Guid>("GuidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Guid_ID");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ShiftGuidId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("Start_date");

                    b.HasKey("GuidId");

                    b.HasIndex("ShiftGuidId");

                    b.HasIndex("StartDate")
                        .IsUnique();

                    b.ToTable("Shifts", "dbo", t =>
                        {
                            t.HasComment("Tabulka slouží k naplánování směny.");
                        });
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

                    b.HasOne("TimerTracker.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftGuidId");

                    b.Navigation("Activity");

                    b.Navigation("Project");

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("TimerTracker.Models.Shift", b =>
                {
                    b.HasOne("TimerTracker.Models.Shift", null)
                        .WithMany("Shifts")
                        .HasForeignKey("ShiftGuidId");
                });

            modelBuilder.Entity("TimerTracker.Models.Activity", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("TimerTracker.Models.Project", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("TimerTracker.Models.Shift", b =>
                {
                    b.Navigation("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}
