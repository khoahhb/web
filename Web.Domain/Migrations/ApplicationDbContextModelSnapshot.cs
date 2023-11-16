﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web.Domain.Context;

#nullable disable

namespace Web.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Web.Domain.Entities.Avatar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<long?>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<bool?>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("MimeType")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Avatars");
                });

            modelBuilder.Entity("Web.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<Guid?>("AvatarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7e42633e-d714-406f-98d6-81909a4502c9"),
                            Address = "Hau Giang",
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            DateOfBirth = new DateTime(2001, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            Email = "admin1@gmail.com",
                            Fullname = "Huynh Huu Bao Khoa",
                            Gender = 0,
                            Password = "UhywqEJrt+FqDqbLZXUxMQ==.fzrKPpUbz3nW+vOP4db3qeUz8eBNVhzSWSXXcSHSX9M=",
                            Phone = "0372753988",
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa",
                            UserProfileId = new Guid("33fb0ec0-6aac-4f83-98bb-5d948e165aa3"),
                            Username = "admin1"
                        },
                        new
                        {
                            Id = new Guid("3583a3a3-5016-4cdc-a794-619e013ca0fc"),
                            Address = "2, Cai Tac",
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            DateOfBirth = new DateTime(2001, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            Email = "Teacher1@gmail.com",
                            Fullname = "Le Thi Thu Hong",
                            Gender = 1,
                            Password = "e9AkXS8u7tgxEBgkGDhHEg==.CIbRSX6JCAcaklyulng1C8FEHwkbMUmxAa0TgM14+wA=",
                            Phone = "0917437736",
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa",
                            UserProfileId = new Guid("6eca6833-37a2-4404-8152-a6b39289a05a"),
                            Username = "Teacher1"
                        },
                        new
                        {
                            Id = new Guid("41ec17a9-bc09-4f92-983f-04ca1a0acb4f"),
                            Address = "Hau Giang",
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            DateOfBirth = new DateTime(2001, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            Email = "Teacher2@gmail.com",
                            Fullname = "Pham Nguyen Khang",
                            Gender = 0,
                            Password = "YjGWyFSr3gpM8YsQMTR32w==.3WEuc4BRzEbhw5VrNC8J+d/7EGYUNvVHZXpkXtRObq8=",
                            Phone = "0917431136",
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa",
                            UserProfileId = new Guid("6eca6833-37a2-4404-8152-a6b39289a05a"),
                            Username = "Teacher2"
                        },
                        new
                        {
                            Id = new Guid("5b7742bd-3b3d-478f-90d5-9e1d590f14a9"),
                            Address = "Hau Giang",
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            DateOfBirth = new DateTime(2001, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            Email = "Student1@gmail.com",
                            Fullname = "Chau Ngoc Hung",
                            Gender = 0,
                            Password = "fBoPmwRGGn2bUgwS8C3F9g==.ucST4KNOgwC34qikVODkcgiFgeu9qAEFU2RBKZ5BkLU=",
                            Phone = "0202431136",
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa",
                            UserProfileId = new Guid("7284e6bc-5913-4dcf-8229-b86a5f52b565"),
                            Username = "Student1"
                        },
                        new
                        {
                            Id = new Guid("1a51b9bd-340a-4b6f-add7-3b5f27fc2387"),
                            Address = "Sai Gon",
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            DateOfBirth = new DateTime(2001, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            Email = "Student2@gmail.com",
                            Fullname = "Ho Vinh Duy",
                            Gender = 0,
                            Password = "4GcJL/PZJ4WGD1xD/zBh+Q==.rPW9T/NlySewoKzvuvenjHXV58chrv5VBlAHZPAv8Io=",
                            Phone = "0209831136",
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa",
                            UserProfileId = new Guid("7284e6bc-5913-4dcf-8229-b86a5f52b565"),
                            Username = "Student2"
                        });
                });

            modelBuilder.Entity("Web.Domain.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Descrtiption")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("33fb0ec0-6aac-4f83-98bb-5d948e165aa3"),
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            Descrtiption = "This is profile for admin.",
                            Name = "Admin Profile",
                            Type = 2,
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa"
                        },
                        new
                        {
                            Id = new Guid("6eca6833-37a2-4404-8152-a6b39289a05a"),
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            Descrtiption = "This is profile for teacher.",
                            Name = "Teacher Profile",
                            Type = 0,
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa"
                        },
                        new
                        {
                            Id = new Guid("7284e6bc-5913-4dcf-8229-b86a5f52b565"),
                            CreatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            CreatedBy = "Huynh Huu Bao Khoa",
                            Descrtiption = "This is profile for student.",
                            Name = "Student Profile",
                            Type = 1,
                            UpdatedAt = new DateTime(2023, 11, 2, 12, 12, 12, 0, DateTimeKind.Utc),
                            UpdatedBy = "Huynh Huu Bao Khoa"
                        });
                });

            modelBuilder.Entity("Web.Domain.Entities.User", b =>
                {
                    b.HasOne("Web.Domain.Entities.Avatar", "Avatar")
                        .WithMany("Users")
                        .HasForeignKey("AvatarId");

                    b.HasOne("Web.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany("Users")
                        .HasForeignKey("UserProfileId");

                    b.Navigation("Avatar");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Web.Domain.Entities.Avatar", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Web.Domain.Entities.UserProfile", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
