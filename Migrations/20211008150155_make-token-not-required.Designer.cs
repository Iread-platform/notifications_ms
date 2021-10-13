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
    [Migration("20211008150155_make-token-not-required")]
    partial class maketokennotrequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.Property<int>("TopicsTopicId")
                        .HasColumnType("int");

                    b.Property<string>("UsersUserId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("TopicsTopicId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("TopicUser");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.SingleNotification", b =>
                {
                    b.Property<int>("SingleNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("ExtraData")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("SendAfter")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(767)");

                    b.HasKey("SingleNotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("SingleNotifications");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.TopicNotification", b =>
                {
                    b.Property<int>("TopicNotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("ExtraData")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("SendAfter")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("TopicNotificationId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicNotifications");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.HasOne("iread_notifications_ms.DataAccess.Data.Entity.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iread_notifications_ms.DataAccess.Data.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.SingleNotification", b =>
                {
                    b.HasOne("iread_notifications_ms.DataAccess.Data.Entity.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.TopicNotification", b =>
                {
                    b.HasOne("iread_notifications_ms.DataAccess.Data.Entity.Topic", "Topic")
                        .WithMany("TopicNotification")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.Topic", b =>
                {
                    b.Navigation("TopicNotification");
                });

            modelBuilder.Entity("iread_notifications_ms.DataAccess.Data.Entity.User", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
