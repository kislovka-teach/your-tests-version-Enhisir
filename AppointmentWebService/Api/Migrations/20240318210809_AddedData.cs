using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "bold",
                column: "PasswordHashed",
                value: "hZb4TdtcNFljt1J3QhimHA==;5D30Y8voGyIgTATupppg6i9moLRaJCl7LAqnPzH4Tig=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "enhisir",
                column: "PasswordHashed",
                value: "1Clhv36s1z/x1O5WSV/ewQ==;BbdPxGKK4VXjTchKMgmW4FFQlhuL8Nr1fQBEKp56j54=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "nikoimam",
                column: "PasswordHashed",
                value: "EmEggxeYaYzU0qMrEUgSuQ==;FV44VkfhLoZ2nCn/J5CNMka2oC9xqjmB0H5tJWIAwZg=");

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 3, 18, 21, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "bold",
                column: "PasswordHashed",
                value: "J1NJIpM6D1tkcaWhtVQhVQ==;f9pTe3apul/7M26ojIEqanyjAve+BTYrTWxtD/Cyamk=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "enhisir",
                column: "PasswordHashed",
                value: "xKfyc6UJU+j81DGbZfEp1A==;Qy/mYbphHMto8TkHn1zO4pXBX8P9XjY89SfpclU3emI=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserName",
                keyValue: "nikoimam",
                column: "PasswordHashed",
                value: "ESxNKbSoQMk6YKuGun1Uog==;p9vRnAUcjBUGN1oKPUXVwTXeqQXeuzxfYHimSKrQ6gE=");

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 3, 19, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
