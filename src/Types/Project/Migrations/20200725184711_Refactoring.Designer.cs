﻿// <auto-generated />
using System;
using HotChocolate.Types.Benchmarks.Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotChocolate.ConferencePlanner.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200725184711_Refactoring")]
    partial class Refactoring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abstract")
                        .HasColumnType("TEXT")
                        .HasMaxLength(4000);

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<int?>("TrackId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.SessionAttendee", b =>
                {
                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttendeeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId", "AttendeeId");

                    b.HasIndex("AttendeeId");

                    b.ToTable("SessionAttendee");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.SessionSpeaker", b =>
                {
                    b.Property<int>("SessionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpeakerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SessionSpeaker");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT")
                        .HasMaxLength(4000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("WebSite")
                        .HasColumnType("TEXT")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.Session", b =>
                {
                    b.HasOne("HotChocolate.ConferencePlanner.Data.Track", "Track")
                        .WithMany("Sessions")
                        .HasForeignKey("TrackId");
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.SessionAttendee", b =>
                {
                    b.HasOne("HotChocolate.ConferencePlanner.Data.Attendee", "Attendee")
                        .WithMany("SessionsAttendees")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotChocolate.ConferencePlanner.Data.Session", "Session")
                        .WithMany("SessionAttendees")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotChocolate.ConferencePlanner.Data.SessionSpeaker", b =>
                {
                    b.HasOne("HotChocolate.ConferencePlanner.Data.Session", "Session")
                        .WithMany("SessionSpeakers")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotChocolate.ConferencePlanner.Data.Speaker", "Speaker")
                        .WithMany("SessionSpeakers")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
