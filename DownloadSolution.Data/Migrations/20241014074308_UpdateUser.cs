using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DownloadSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash" },
                values: new object[] { "6ca35423-0bee-4dcf-9c7b-12e549a39c18", 0, "AQAAAAIAAYagAAAAEJLu3PVhKIQgp23oIzgjtxQBD5Osup7NkqRu4jeQk55nfs/2s/jFzq1+1urYyiKrKg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1c0410d2-0a99-4238-8b1f-af85a5293510", "AQAAAAIAAYagAAAAEBKmo6u5TT6r4sPpObHCx/aAjI4qM6e/trU0CGiKPEh3BOsgn9uc3a4t5uJ41+SLWg==" });
        }
    }
}
