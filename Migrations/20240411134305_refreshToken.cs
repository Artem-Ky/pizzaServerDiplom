using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pizzaServerApp.Migrations
{
    /// <inheritdoc />
    public partial class refreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "196b63c1-9ec9-4e77-9834-9a79a2721e41");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bee5e1fd-b3f1-4f1b-bd47-867d76324613");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "public",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                schema: "public",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15cd6f21-3c7b-4bef-a152-d9d22e73aaed", null, "User", "USER" },
                    { "f805e1d9-a369-4216-b731-beea8fdaa75b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15cd6f21-3c7b-4bef-a152-d9d22e73aaed");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f805e1d9-a369-4216-b731-beea8fdaa75b");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "public",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                schema: "public",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "196b63c1-9ec9-4e77-9834-9a79a2721e41", null, "Admin", "ADMIN" },
                    { "bee5e1fd-b3f1-4f1b-bd47-867d76324613", null, "User", "USER" }
                });
        }
    }
}
