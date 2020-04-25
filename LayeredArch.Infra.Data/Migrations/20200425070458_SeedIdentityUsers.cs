using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LayeredArch.Infra.Data.Migrations
{
    public partial class SeedIdentityUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301d884-221a-4e7d-b509-0113dcc043E1",
                column: "ConcurrencyStamp",
                value: "6d0f00f3-0e0e-4581-9bbc-7aaafa1e8fe7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d9b7113-a8f8-4035-99a7-a20dd400f6a3",
                column: "ConcurrencyStamp",
                value: "87a8e95d-13a9-4fd6-9121-b0e241952ff4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "9e10de69-c41e-4238-b103-c6584f8a602e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "01b168fe-810b-432d-9010-233ba0b380e9", 0, "67f0fe57-9574-4347-b726-8bde5f95578c", new DateTime(2020, 4, 25, 10, 4, 58, 8, DateTimeKind.Local).AddTicks(8269), "admin@admin.com", true, "Super", true, "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEBLMzwr3gIbvvp2IeBQfT1BKusjexuDC+SdyrKhI0phgmf2obYXdwRXPpbuB2g2TBA==", "000-000-0000", true, "e5a2345d-9802-477a-b2aa-afbe4a6bff93", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01b168fe-810b-432d-9010-233ba0b380e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301d884-221a-4e7d-b509-0113dcc043E1",
                column: "ConcurrencyStamp",
                value: "dd055320-67b3-4fca-8fd2-df808bdd707a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d9b7113-a8f8-4035-99a7-a20dd400f6a3",
                column: "ConcurrencyStamp",
                value: "82287d55-6bfe-4a1d-8c22-9f102953c3a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "ad68046a-e78c-4082-ba01-36126130c89e");
        }
    }
}
