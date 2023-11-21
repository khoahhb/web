using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Descrtiption = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Profiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Descrtiption", "IsDeleted", "Name", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("33fb0ec0-6aac-4f83-98bb-5d948e165aa3"), new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), "This is profile for admin.", false, "Admin Profile", 2, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9") },
                    { new Guid("6eca6833-37a2-4404-8152-a6b39289a05a"), new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), "This is profile for teacher.", false, "Teacher Profile", 0, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9") },
                    { new Guid("7284e6bc-5913-4dcf-8229-b86a5f52b565"), new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), "This is profile for student.", false, "Student Profile", 1, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarId", "CreatedAt", "CreatedBy", "DateOfBirth", "Email", "Fullname", "Gender", "IsDeleted", "Password", "Phone", "UpdatedAt", "UpdatedBy", "UserProfileId", "Username" },
                values: new object[,]
                {
                    { new Guid("1a51b9bd-340a-4b6f-add7-3b5f27fc2387"), "Sai Gon", null, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new DateTime(2001, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), "Student2@gmail.com", "Ho Vinh Duy", 0, false, "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=", "0209831136", new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new Guid("7284e6bc-5913-4dcf-8229-b86a5f52b565"), "Student2" },
                    { new Guid("3583a3a3-5016-4cdc-a794-619e013ca0fc"), "2, Cai Tac", null, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new DateTime(2001, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), "Teacher1@gmail.com", "Le Thi Thu Hong", 1, false, "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=", "0917437736", new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new Guid("6eca6833-37a2-4404-8152-a6b39289a05a"), "Teacher1" },
                    { new Guid("41ec17a9-bc09-4f92-983f-04ca1a0acb4f"), "Hau Giang", null, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new DateTime(2001, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), "Teacher2@gmail.com", "Pham Nguyen Khang", 0, false, "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=", "0917431136", new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new Guid("6eca6833-37a2-4404-8152-a6b39289a05a"), "Teacher2" },
                    { new Guid("5b7742bd-3b3d-478f-90d5-9e1d590f14a9"), "Hau Giang", null, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new DateTime(2001, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), "Student1@gmail.com", "Chau Ngoc Hung", 0, false, "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=", "0202431136", new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new Guid("7284e6bc-5913-4dcf-8229-b86a5f52b565"), "Student1" },
                    { new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), "Hau Giang", null, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new DateTime(2001, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), "admin1@gmail.com", "Huynh Huu Bao Khoa", 0, false, "JqUI1dEGEKTvyxJ8T6rK4w==.tx3AE07BC5n6jkF70x7odozYEejXNkjqEU9DFmpNxfA=", "0372753988", new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("7e42633e-d714-406f-98d6-81909a4502c9"), new Guid("33fb0ec0-6aac-4f83-98bb-5d948e165aa3"), "admin1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_UserId",
                table: "Credentials",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserProfileId",
                table: "Users",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
