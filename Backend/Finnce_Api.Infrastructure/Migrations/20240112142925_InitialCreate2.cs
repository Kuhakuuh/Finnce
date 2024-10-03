using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinnceApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd05b37f-3ba7-4d90-9105-a6a91744b410");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "40b6aca6-299d-41be-a778-d536e2dd1940", "6586d86b-5194-410c-a769-df75a88f2da3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40b6aca6-299d-41be-a778-d536e2dd1940");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6586d86b-5194-410c-a769-df75a88f2da3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fe40072-0989-4990-a6df-159ac7b82a08", "0325814b-201d-4a33-860a-6bae511e41cb", "admin", "admin" },
                    { "8726e534-0650-40ae-b1d8-17c23dc822e9", "39e3314b-8d2b-43a0-ba1d-27f8cbb6e792", "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "User", "UserName" },
                values: new object[] { "6fc094bb-ffb5-4ff6-835c-f69b3bf405b8", 0, "e0fb47de-7ee3-4a63-8f0f-eb9df3795e27", "admin@gmail.com", false, false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEDRJp03nepZ6dNiNIpRVVkDk/5AqPI6JmthFGvQE+EyWi2h6A59uM9v4rdmBCZs4Lg==", null, false, "", false, "IdentityUser", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2fe40072-0989-4990-a6df-159ac7b82a08", "6fc094bb-ffb5-4ff6-835c-f69b3bf405b8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8726e534-0650-40ae-b1d8-17c23dc822e9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2fe40072-0989-4990-a6df-159ac7b82a08", "6fc094bb-ffb5-4ff6-835c-f69b3bf405b8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fe40072-0989-4990-a6df-159ac7b82a08");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fc094bb-ffb5-4ff6-835c-f69b3bf405b8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40b6aca6-299d-41be-a778-d536e2dd1940", "128b4e8e-3b3c-4752-8bda-a198f4f4960f", "admin", "admin" },
                    { "cd05b37f-3ba7-4d90-9105-a6a91744b410", "1495e2b0-bcdc-4f59-a770-26fc17121d6c", "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "User", "UserName" },
                values: new object[] { "6586d86b-5194-410c-a769-df75a88f2da3", 0, "ce672d1b-e055-440b-965c-fb680beb2b66", "admin@gmail.com", false, false, null, "admin@gmail.com", "admin", "AQAAAAEAACcQAAAAEEzpjFTWbo6x5+vQM8bMSmjHEn40taLAtk3jeIZmVb8byQjHMRcsxVP4gf6e0Vupdw==", null, false, "", false, "IdentityUser", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "40b6aca6-299d-41be-a778-d536e2dd1940", "6586d86b-5194-410c-a769-df75a88f2da3" });
        }
    }
}
