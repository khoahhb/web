using Microsoft.EntityFrameworkCore;
using Web.Domain.Entities;
using Web.Model.EnumerationTypes;

namespace Web.Domain.Context
{
    public class DataSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public DataSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var dateC = new DateTime(2023, 11, 02, 12, 00, 00, DateTimeKind.Utc);

            var id1 = Guid.Parse("33fb0ec0-6aac-4f83-98bb-5d948e165aa3");
            var id2 = Guid.Parse("6eca6833-37a2-4404-8152-a6b39289a05a");
            var id3 = Guid.Parse("7284e6bc-5913-4dcf-8229-b86a5f52b565");
            var id4 = Guid.Parse("7e42633e-d714-406f-98d6-81909a4502c9");
            var id5 = Guid.Parse("3583a3a3-5016-4cdc-a794-619e013ca0fc");
            var id6 = Guid.Parse("6eca6833-37a2-4404-8152-a6b39289a05a");
            var id7 = Guid.Parse("41ec17a9-bc09-4f92-983f-04ca1a0acb4f");
            var id8 = Guid.Parse("5b7742bd-3b3d-478f-90d5-9e1d590f14a9");
            var id9 = Guid.Parse("1a51b9bd-340a-4b6f-add7-3b5f27fc2387");

            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile
                {
                    Id = id1,
                    Name = "Admin Profile",
                    Type = ProfileType.Admin,
                    Descrtiption = "This is profile for admin.",
                    CreatedAt = dateC,
                    UpdatedAt = dateC,
                    CreatedBy = id4,
                    UpdatedBy = id4,
                },
                 new UserProfile
                 {
                     Id = id2,
                     Name = "Teacher Profile",
                     Type = ProfileType.Teacher,
                     Descrtiption = "This is profile for teacher.",
                     CreatedAt = dateC,
                     UpdatedAt = dateC,
                     CreatedBy = id4,
                     UpdatedBy = id4,
                 },
                 new UserProfile
                 {
                     Id = id3,
                     Name = "Student Profile",
                     Type = ProfileType.Student,
                     Descrtiption = "This is profile for student.",
                     CreatedAt = dateC,
                     UpdatedAt = dateC,
                     CreatedBy = id4,
                     UpdatedBy = id4,
                 }
            );
            var test = new DateTime(2001, 11, 02, 12, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = id4,
                    Username = "admin1",
                    Password = "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=",
                    Fullname = "Huynh Huu Bao Khoa",
                    Gender = GenderType.Male,
                    DateOfBirth = test,
                    Phone = "0372753988",
                    Email = "admin1@gmail.com",
                    Address = "Hau Giang",
                    CreatedAt = dateC,
                    UpdatedAt = dateC,
                    CreatedBy = id4,
                    UpdatedBy = id4,
                    UserProfileId = id1,
                },
                new User
                {
                    Id = id5,
                    Username = "Teacher1",
                    Password = "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=",
                    Fullname = "Le Thi Thu Hong",
                    Gender = GenderType.Female,
                    DateOfBirth = test,
                    Phone = "0917437736",
                    Email = "Teacher1@gmail.com",
                    Address = "2, Cai Tac",
                    CreatedAt = dateC,
                    UpdatedAt = dateC,
                    CreatedBy = id4,
                    UpdatedBy = id4,
                    UserProfileId = id2,
                },
                new User
                {
                    Id = id7,
                    Username = "Teacher2",
                    Password = "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=",
                    Fullname = "Pham Nguyen Khang",
                    Gender = GenderType.Male,
                    DateOfBirth = test,
                    Phone = "0917431136",
                    Email = "Teacher2@gmail.com",
                    Address = "Hau Giang",
                    CreatedAt = dateC,
                    UpdatedAt = dateC,
                    CreatedBy = id4,
                    UpdatedBy = id4,
                    UserProfileId = id2,
                },
                new User
                {
                    Id = id8,
                    Username = "Student1",
                    Password = "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=",
                    Fullname = "Chau Ngoc Hung",
                    Gender = GenderType.Male,
                    DateOfBirth = test,
                    Phone = "0202431136",
                    Email = "Student1@gmail.com",
                    Address = "Hau Giang",
                    CreatedAt = dateC,
                    UpdatedAt = dateC,
                    CreatedBy = id4,
                    UpdatedBy = id4,
                    UserProfileId = id3,
                },
                new User
                {
                    Id = id9,
                    Username = "Student2",
                    Password = "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=",
                    Fullname = "Ho Vinh Duy",
                    Gender = GenderType.Male,
                    DateOfBirth = test,
                    Phone = "0209831136",
                    Email = "Student2@gmail.com",
                    Address = "Sai Gon",
                    CreatedAt = dateC,
                    UpdatedAt = dateC,
                    CreatedBy = id4,
                    UpdatedBy = id4,
                    UserProfileId = id3,
                }
            );
        }
    }
}
