using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UpdatedAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mille.com", "Admin", "$2a$11$E3RGRhjfkGzTz5J42JIOXe3dpiCEGaiZZxLIYfm0qdwnc/xFU/w.u", null, null, null, 0, new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
