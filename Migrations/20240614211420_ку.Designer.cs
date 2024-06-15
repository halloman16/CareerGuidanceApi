﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.src.Infrastructure.Data;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240614211420_ку")]
    partial class ку
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webapi.src.Domain.Models.ModuleModel", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserModelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Name");

                    b.HasIndex("UserModelId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.SessionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<float>("Mark")
                        .HasColumnType("real");

                    b.Property<float>("MaxScore")
                        .HasColumnType("real");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.Property<Guid>("UserModuleSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserModuleSessionId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecoveryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RecoveryCodeValidBefore")
                        .HasColumnType("datetime2");

                    b.Property<string>("RestoreCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RestoreCodeValidBefore")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("TokenValidBefore")
                        .HasColumnType("datetime2");

                    b.Property<bool>("WasPasswordResetRequest")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Token")
                        .IsUnique()
                        .HasFilter("[Token] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.UserModuleSessionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ModuleName");

                    b.HasIndex("UserId");

                    b.ToTable("UserModuleSessions");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.ModuleModel", b =>
                {
                    b.HasOne("webapi.src.Domain.Models.UserModel", null)
                        .WithMany("CreatedModules")
                        .HasForeignKey("UserModelId");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.SessionModel", b =>
                {
                    b.HasOne("webapi.src.Domain.Models.UserModuleSessionModel", "UserModuleSession")
                        .WithMany("Sessions")
                        .HasForeignKey("UserModuleSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModuleSession");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.UserModuleSessionModel", b =>
                {
                    b.HasOne("webapi.src.Domain.Models.ModuleModel", "Module")
                        .WithMany("UserModuleSessions")
                        .HasForeignKey("ModuleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.src.Domain.Models.UserModel", "User")
                        .WithMany("UserModuleSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.ModuleModel", b =>
                {
                    b.Navigation("UserModuleSessions");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.UserModel", b =>
                {
                    b.Navigation("CreatedModules");

                    b.Navigation("UserModuleSessions");
                });

            modelBuilder.Entity("webapi.src.Domain.Models.UserModuleSessionModel", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
