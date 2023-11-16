﻿using Microsoft.EntityFrameworkCore;
using Web.Domain.Test.User;
using Web.Domain.Test.UserProfile;
using Web.Model.Enum;

namespace Web.Domain.Test.Context
{
    public class DataSeederTest
    {
        private readonly ModelBuilder modelBuilder;

        public DataSeederTest(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var adminName = "Huynh Huu Bao Khoa";
            var dateC = new DateTime(2023, 11, 02, 12, 12, 12, DateTimeKind.Utc);

            var id1 = Guid.Parse("33fb0ec0-6aac-4f83-98bb-5d948e165aa3");
            var id2 = Guid.Parse("6eca6833-37a2-4404-8152-a6b39289a05a");
            var id3 = Guid.Parse("7284e6bc-5913-4dcf-8229-b86a5f52b565");
            var id4 = Guid.Parse("7e42633e-d714-406f-98d6-81909a4502c9");
            var id5 = Guid.Parse("3583a3a3-5016-4cdc-a794-619e013ca0fc");
            var id6 = Guid.Parse("6eca6833-37a2-4404-8152-a6b39289a05a");
            var id7 = Guid.Parse("41ec17a9-bc09-4f92-983f-04ca1a0acb4f");
            var id8 = Guid.Parse("5b7742bd-3b3d-478f-90d5-9e1d590f14a9");
            var id9 = Guid.Parse("1a51b9bd-340a-4b6f-add7-3b5f27fc2387");

            modelBuilder.Entity<UserProfileEntity>().HasData(
                new UserProfileEntity
                {
                    Id = id1,
                    Name = "Admin Profile",
                    Type = ProfileType.Admin,
                    Descrtiption = "This is profile for admin.",
                    CreatedDate = dateC,
                    UpdatedDate = dateC,
                    CreatedBy = adminName,
                    UpdatedBy = adminName,
                },
                 new UserProfileEntity
                 {
                     Id = id2,
                     Name = "Teacher Profile",
                     Type = ProfileType.Teacher,
                     Descrtiption = "This is profile for teacher.",
                     CreatedDate = dateC,
                     UpdatedDate = dateC,
                     CreatedBy = adminName,
                     UpdatedBy = adminName,
                 },
                 new UserProfileEntity
                 {
                     Id = id3,
                     Name = "Student Profile",
                     Type = ProfileType.Student,
                     Descrtiption = "This is profile for student.",
                     CreatedDate = dateC,
                     UpdatedDate = dateC,
                     CreatedBy = adminName,
                     UpdatedBy = adminName,
                 }
            );
            var test = new DateTime(2001, 11, 02, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = id4,
                    Username = "admin1",
                    Password = "UhywqEJrt+FqDqbLZXUxMQ==.fzrKPpUbz3nW+vOP4db3qeUz8eBNVhzSWSXXcSHSX9M=",
                    Fullname = "Huynh Huu Bao Khoa",
                    Gender = GenderType.Male,
                    DateOfBirth = new DateTime(2001, 11, 02, 12, 12, 12, DateTimeKind.Utc),
                    Phone = "0372753988",
                    Email = "admin1@gmail.com",
                    Address = "Hau Giang",
                    CreatedDate = dateC,
                    UpdatedDate = dateC,
                    CreatedBy = adminName,
                    UpdatedBy = adminName,
                    UserProfileId = id1,
                },
                new UserEntity
                {
                    Id = id5,
                    Username = "Teacher1",
                    Password = "e9AkXS8u7tgxEBgkGDhHEg==.CIbRSX6JCAcaklyulng1C8FEHwkbMUmxAa0TgM14+wA=",
                    Fullname = "Le Thi Thu Hong",
                    Gender = GenderType.Female,
                    DateOfBirth = new DateTime(2001, 11, 02, 12, 12, 12, DateTimeKind.Utc),
                    Phone = "0917437736",
                    Email = "Teacher1@gmail.com",
                    Address = "2, Cai Tac",
                    CreatedDate = dateC,
                    UpdatedDate = dateC,
                    CreatedBy = adminName,
                    UpdatedBy = adminName,
                    UserProfileId = id2,
                },
                new UserEntity
                {
                    Id = id7,
                    Username = "Teacher2",
                    Password = "YjGWyFSr3gpM8YsQMTR32w==.3WEuc4BRzEbhw5VrNC8J+d/7EGYUNvVHZXpkXtRObq8=",
                    Fullname = "Pham Nguyen Khang",
                    Gender = GenderType.Male,
                    DateOfBirth = new DateTime(2001, 11, 02, 12, 12, 12, DateTimeKind.Utc),
                    Phone = "0917431136",
                    Email = "Teacher2@gmail.com",
                    Address = "Hau Giang",
                    CreatedDate = dateC,
                    UpdatedDate = dateC,
                    CreatedBy = adminName,
                    UpdatedBy = adminName,
                    UserProfileId = id2,
                },
                new UserEntity
                {
                    Id = id8,
                    Username = "Student1",
                    Password = "fBoPmwRGGn2bUgwS8C3F9g==.ucST4KNOgwC34qikVODkcgiFgeu9qAEFU2RBKZ5BkLU=",
                    Fullname = "Chau Ngoc Hung",
                    Gender = GenderType.Male,
                    DateOfBirth = new DateTime(2001, 11, 02, 12, 12, 12, DateTimeKind.Utc),
                    Phone = "0202431136",
                    Email = "Student1@gmail.com",
                    Address = "Hau Giang",
                    CreatedDate = dateC,
                    UpdatedDate = dateC,
                    CreatedBy = adminName,
                    UpdatedBy = adminName,
                    UserProfileId = id3,
                },
                new UserEntity
                {
                    Id = id9,
                    Username = "Student2",
                    Password = "4GcJL/PZJ4WGD1xD/zBh+Q==.rPW9T/NlySewoKzvuvenjHXV58chrv5VBlAHZPAv8Io=",
                    Fullname = "Ho Vinh Duy",
                    Gender = GenderType.Male,
                    DateOfBirth = new DateTime(2001, 11, 02, 12, 12, 12, DateTimeKind.Utc),
                    Phone = "0209831136",
                    Email = "Student2@gmail.com",
                    Address = "Sai Gon",
                    CreatedDate = dateC,
                    UpdatedDate = dateC,
                    CreatedBy = adminName,
                    UpdatedBy = adminName,
                    UserProfileId = id3,
                }
            );
        }
    }
}
