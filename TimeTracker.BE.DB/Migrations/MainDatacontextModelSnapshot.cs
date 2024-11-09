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

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("End_time");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Project_ID");

                    b.Property<Guid?>("ShiftGuidId")
                        .HasColumnType("TEXT")
                        .HasColumnName("Shift_GuidID");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("Start_time");

                    b.Property<int?>("SubModuleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("SubModule_ID");

                    b.Property<int>("TypeShiftId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TypeShift_ID");

                    b.HasKey("GuidId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ShiftGuidId");

                    b.HasIndex("SubModuleId");

                    b.HasIndex("TypeShiftId");

                    b.ToTable("Record_activities", "dbo");

                    b.HasData(
                        new
                        {
                            GuidId = new Guid("8abad025-5b76-4892-af0e-d4aeac7997c5"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 1, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("f3f762d8-95df-4167-8adb-81f547e304d9"),
                            ActivityId = 2,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 1, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("bac7229d-440d-4adc-993c-94535bb5e546"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("e7185319-2d67-4f88-83a7-9b8e3944e0ae"),
                            ActivityId = 3,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("aabfd309-0229-47c2-b133-1409ae99cfed"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 2, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("f8310969-4e8e-4c7c-ae78-f3d54b09cbbb"),
                            ActivityId = 2,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 2, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("27ec3b05-1f32-40a3-8d30-0a19f5ebac7a"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("d92177c1-2316-424b-b86c-06d9d19ade48"),
                            ActivityId = 3,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 2, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("6d4818f6-fce2-4bbb-bd66-3aa8af71552a"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 3, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("6f0064d2-dbb6-4efa-9048-8eb00c5ae8b4"),
                            ActivityId = 2,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 3, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("8d9647fc-ac15-49bd-a79f-0026c5f277d5"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("5598ebbb-c462-4eaf-9107-5ba0502badfa"),
                            ActivityId = 3,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 3, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 1
                        },
                        new
                        {
                            GuidId = new Guid("8b2ed31f-ee9f-46af-8aee-fd3776a1c0c1"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 4, 7, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("8cbd3b04-b6b1-4fe4-a6bc-d95b3199ed10"),
                            ActivityId = 2,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 4, 11, 40, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("36779593-4c6c-4ee7-99c4-7f5432307554"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("daa2ed71-cc00-4dea-9260-e3c4183df11d"),
                            ActivityId = 3,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 4, 15, 10, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 2
                        },
                        new
                        {
                            GuidId = new Guid("2bc11d62-a5aa-4717-a255-38d623d5ab97"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 5, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 3
                        },
                        new
                        {
                            GuidId = new Guid("be80f7d2-ab43-4fef-a092-9a9763de6779"),
                            ActivityId = 2,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 5, 11, 40, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 3
                        },
                        new
                        {
                            GuidId = new Guid("b7fbaf6d-7523-43e4-af03-b64775dbcd0c"),
                            ActivityId = 1,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 3
                        },
                        new
                        {
                            GuidId = new Guid("fdb64dce-9253-400d-a094-b2a332b77f80"),
                            ActivityId = 3,
                            Description = "",
                            StartTime = new DateTime(2024, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            TypeShiftId = 3
                        });
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
                        .HasForeignKey("TypeShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
