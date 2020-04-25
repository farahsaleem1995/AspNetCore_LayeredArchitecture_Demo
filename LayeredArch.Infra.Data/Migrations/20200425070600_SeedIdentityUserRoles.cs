using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LayeredArch.Infra.Data.Migrations
{
    public partial class SeedIdentityUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2301d884-221a-4e7d-b509-0113dcc043E1",
                column: "ConcurrencyStamp",
                value: "02455558-dbd3-4ef3-938a-6cd995a40dc1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d9b7113-a8f8-4035-99a7-a20dd400f6a3",
                column: "ConcurrencyStamp",
                value: "023bcca2-a0bd-4c9e-bcc9-0b75bec5f6e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "76a3286f-5b95-46bc-950e-2bca61407c72");

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
                values: new object[] { "e806fac7-57f4-4998-af12-2d81aac0a489", new DateTime(2020, 4, 25, 10, 6, 0, 48, DateTimeKind.Local).AddTicks(8729), "AQAAAAEAACcQAAAAEGLn7DFunlWhoBCyUYsYBRu/NY0e6EXX7TaMIjG9L9bLHhg60lBOT6wmGha4C6bbNw==", "badbde7f-0984-4975-af13-80c4b202f252" });
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01b168fe-810b-432d-9010-233ba0b380e9",
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67f0fe57-9574-4347-b726-8bde5f95578c", new DateTime(2020, 4, 25, 10, 4, 58, 8, DateTimeKind.Local).AddTicks(8269), "AQAAAAEAACcQAAAAEBLMzwr3gIbvvp2IeBQfT1BKusjexuDC+SdyrKhI0phgmf2obYXdwRXPpbuB2g2TBA==", "e5a2345d-9802-477a-b2aa-afbe4a6bff93" });
        }
    }
}
