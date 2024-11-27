﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTracker.BE.DB.DataAccess;

#nullable disable

namespace TimeTracker.BE.DB.Migrations
{
    [DbContext(typeof(MainDatacontext))]
    partial class MainDatacontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Activity_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

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

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Project", b =>
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

                    b.HasIndex("Name")
                        .IsUnique();

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

            modelBuilder.Entity("TimeTracker.BE.DB.Models.RecordActivity", b =>
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

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("End_DateTime");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Project_ID");

                    b.Property<Guid?>("ShiftGuidId")
                        .HasColumnType("TEXT")
                        .HasColumnName("Shift_GuidID");

                    b.Property<DateTime>("StampDateTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("Stamp_DateTime");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("Start_DateTime");

                    b.Property<int?>("SubModuleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("SubModule_ID");

                    b.Property<int?>("TypeShiftId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TypeShift_ID");

                    b.HasKey("GuidId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ShiftGuidId");

                    b.HasIndex("SubModuleId");

                    b.HasIndex("TypeShiftId");

                    b.ToTable("Record_activities", "dbo");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Shift", b =>
                {
                    b.Property<Guid>("GuidId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Guid_ID");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ShiftGuidId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StampDateTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("Stamp_DateTime");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("Start_date");

                    b.Property<int>("TypeShiftId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TypeShift_ID");

                    b.HasKey("GuidId");

                    b.HasIndex("ShiftGuidId");

                    b.HasIndex("StartDate")
                        .IsUnique();

                    b.HasIndex("TypeShiftId");

                    b.ToTable("Shifts", "dbo", t =>
                        {
                            t.HasComment("Tabulka slouží k naplánování směny.");
                        });

                    b.HasData(
                        new
                        {
                            GuidId = new Guid("fa1db3cb-f2c4-4efd-aee9-cd3487366229"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8748),
                            StartDate = new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("9ad8ec6d-6bd8-4cf5-b681-c46c86c508f3"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8767),
                            StartDate = new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("d917a220-5a2c-401c-90cc-c746aaada412"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8777),
                            StartDate = new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("68dbc51a-9546-4450-bf46-e614397021e4"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8787),
                            StartDate = new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("31402227-1064-4455-872f-df218a85aca3"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8796),
                            StartDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("d434a034-f93d-4f68-a69a-60243a16d21d"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8805),
                            StartDate = new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("a634ce0f-ab0c-4061-babb-70f656277fa1"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8814),
                            StartDate = new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 3
                        },
                        new
                        {
                            GuidId = new Guid("865370c2-1115-4b23-b4c9-f7cdebbbe86d"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8823),
                            StartDate = new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("a9f6f060-c255-43e5-b5d5-e9d7860ab14d"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8831),
                            StartDate = new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("d10f4bf4-c1a4-404b-9213-803ad4cee509"),
                            StampDateTime = new DateTime(2024, 11, 27, 22, 40, 14, 639, DateTimeKind.Local).AddTicks(8840),
                            StartDate = new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        });
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.SubModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("SubModule_ID");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Project_ID");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId", "Name")
                        .IsUnique();

                    b.ToTable("SubModule", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SubModule 1",
                            ProjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "SubModule 2",
                            ProjectId = 1
                        });
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.TypeShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("TypeShift_ID");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisibleInMainWindow")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<int?>("TypeShiftId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TypeShiftId");

                    b.ToTable("TypeShifts", "dbo", t =>
                        {
                            t.HasComment("Tabulka všech možných směn.");
                        });

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Color = "SkyBlue",
                            IsVisibleInMainWindow = true,
                            Name = "HomeOffice"
                        },
                        new
                        {
                            Id = 1,
                            Color = "Orange",
                            IsVisibleInMainWindow = true,
                            Name = "Office"
                        },
                        new
                        {
                            Id = 3,
                            Color = "Magenta",
                            IsVisibleInMainWindow = true,
                            Name = "Others"
                        },
                        new
                        {
                            Id = 4,
                            Color = "LawnGreen",
                            IsVisibleInMainWindow = false,
                            Name = "Holiday"
                        });
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.RecordActivity", b =>
                {
                    b.HasOne("TimeTracker.BE.DB.Models.Activity", "Activity")
                        .WithMany("Activities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTracker.BE.DB.Models.Project", "Project")
                        .WithMany("Activities")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TimeTracker.BE.DB.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftGuidId");

                    b.HasOne("TimeTracker.BE.DB.Models.SubModule", "SubModule")
                        .WithMany("Activities")
                        .HasForeignKey("SubModuleId");

                    b.HasOne("TimeTracker.BE.DB.Models.TypeShift", "TypeShift")
                        .WithMany("RecordActivity")
                        .HasForeignKey("TypeShiftId");

                    b.Navigation("Activity");

                    b.Navigation("Project");

                    b.Navigation("Shift");

                    b.Navigation("SubModule");

                    b.Navigation("TypeShift");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Shift", b =>
                {
                    b.HasOne("TimeTracker.BE.DB.Models.Shift", null)
                        .WithMany("Shifts")
                        .HasForeignKey("ShiftGuidId");

                    b.HasOne("TimeTracker.BE.DB.Models.TypeShift", "TypeShift")
                        .WithMany()
                        .HasForeignKey("TypeShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeShift");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.SubModule", b =>
                {
                    b.HasOne("TimeTracker.BE.DB.Models.Project", "Project")
                        .WithMany("SubModules")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.TypeShift", b =>
                {
                    b.HasOne("TimeTracker.BE.DB.Models.TypeShift", null)
                        .WithMany("TypeShifts")
                        .HasForeignKey("TypeShiftId");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Activity", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Project", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("SubModules");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.Shift", b =>
                {
                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.SubModule", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("TimeTracker.BE.DB.Models.TypeShift", b =>
                {
                    b.Navigation("RecordActivity");

                    b.Navigation("TypeShifts");
                });
#pragma warning restore 612, 618
        }
    }
}
