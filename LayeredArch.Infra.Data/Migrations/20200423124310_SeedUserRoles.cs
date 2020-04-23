using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LayeredArch.Infra.Data.Migrations
{
    public partial class SeedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301d884-221a-4e7d-b509-0113dcc043E1",
                column: "ConcurrencyStamp",
                value: "1296e13c-ab7a-4637-937a-f4683588b53d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d9b7113-a8f8-4035-99a7-a20dd400f6a3",
                column: "ConcurrencyStamp",
                value: "61058541-2ebc-458b-abfa-458227cf9277");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "40365790-e65a-45bb-9fb8-943ff82d8b95");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "01b168fe-810b-432d-9010-233ba0b380e9", "a18be9c0-aa65-4af8-bd17-00bd9344e575" },
                    { "01b168fe-810b-432d-9010-233ba0b380e9", "2301d884-221a-4e7d-b509-0113dcc043E1" },
                    { "01b168fe-810b-432d-9010-233ba0b380e9", "7d9b7113-a8f8-4035-99a7-a20dd400f6a3" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01b168fe-810b-432d-9010-233ba0b380e9",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49810cc1-4bc7-4a66-a064-de6da47dd4e1", new DateTime(2020, 4, 23, 15, 43, 9, 578, DateTimeKind.Local).AddTicks(1243), "AQAAAAEAACcQAAAAEIRZA2hdgVObSpHnh+892b7eAyGjXFfD1nI9AV0bMcoQW/SHjWn8IuUMQYm8c5cJ3A==", "59724a69-e59d-4f79-8a8f-84be7ba0f1c3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "01b168fe-810b-432d-9010-233ba0b380e9", "2301d884-221a-4e7d-b509-0113dcc043E1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "01b168fe-810b-432d-9010-233ba0b380e9", "7d9b7113-a8f8-4035-99a7-a20dd400f6a3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "01b168fe-810b-432d-9010-233ba0b380e9", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301d884-221a-4e7d-b509-0113dcc043E1",
                column: "ConcurrencyStamp",
                value: "fc9abc81-01d8-4e4b-b82b-2a7510acb038");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d9b7113-a8f8-4035-99a7-a20dd400f6a3",
                column: "ConcurrencyStamp",
                value: "ea804d5a-7776-4f10-b943-d09875eb7a66");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "4da65403-aa0b-44d8-b8cb-434455ab506d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01b168fe-810b-432d-9010-233ba0b380e9",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dff4456-ad45-44ff-9804-f4f65e3fb51d", new DateTime(2020, 4, 23, 15, 38, 49, 792, DateTimeKind.Local).AddTicks(1754), "AQAAAAEAACcQAAAAEMSSQVt/GpWh/PEc/iVfBD5MmURm4x9ALHLyvTRrvKBzrQ3YV3hlPxyxtPjNtCNhNg==", "a4c63a92-d217-4e1e-8ba2-b7c6041999c2" });
        }
    }
}
