﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iread_notifications_ms.DataAccess;

namespace iread_notifications_ms.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210925102254_Inheritance2")]
    partial class Inheritance2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.BroadcastNotification", b =>
                {
                    b.HasBaseType("iread_notifications_ms.DataAccess.Data.Entity.Notification");

                    b.Property<string>("Topic")
                        .HasColumnType("text");

                    b.ToTable("TopicNotification");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.SingleNotification", b =>
                {
                    b.HasBaseType("iread_notifications_ms.DataAccess.Data.Entity.Notification");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.ToTable("SingleNotifications");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.BroadcastNotification", b =>
                {
                    b.HasOne("iread_notifications_ms.DataAccess.Data.Entity.Notification", null)
                        .WithOne()
                        .HasForeignKey("iread_notifications_ms.DataAccess.Data.Entity.BroadcastNotification", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.SingleNotification", b =>
                {
                    b.HasOne("iread_notifications_ms.DataAccess.Data.Entity.Notification", null)
                        .WithOne()
                        .HasForeignKey("iread_notifications_ms.DataAccess.Data.Entity.SingleNotification", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
