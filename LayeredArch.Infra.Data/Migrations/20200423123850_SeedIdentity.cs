using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LayeredArch.Infra.Data.Migrations
{
    public partial class SeedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "4da65403-aa0b-44d8-b8cb-434455ab506d", "User", "USER" },
                    { "2301d884-221a-4e7d-b509-0113dcc043E1", "fc9abc81-01d8-4e4b-b82b-2a7510acb038", "Admin", "ADMIN" },
                    { "7d9b7113-a8f8-4035-99a7-a20dd400f6a3", "ea804d5a-7776-4f10-b943-d09875eb7a66", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "01b168fe-810b-432d-9010-233ba0b380e9", 0, "6dff4456-ad45-44ff-9804-f4f65e3fb51d", new DateTime(2020, 4, 23, 15, 38, 49, 792, DateTimeKind.Local).AddTicks(1754), "admin@admin.com", true, "Super", true, "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEMSSQVt/GpWh/PEc/iVfBD5MmURm4x9ALHLyvTRrvKBzrQ3YV3hlPxyxtPjNtCNhNg==", "123a123a", true, "a4c63a92-d217-4e1e-8ba2-b7c6041999c2", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301d884-221a-4e7d-b509-0113dcc043E1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d9b7113-a8f8-4035-99a7-a20dd400f6a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01b168fe-810b-432d-9010-233ba0b380e9");
        }
    }
}
