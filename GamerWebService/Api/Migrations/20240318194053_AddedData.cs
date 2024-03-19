using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CD Projekt RED" },
                    { 2, "Konami" },
                    { 3, "Bungie" },
                    { 4, "Xbox Game Studios" },
                    { 5, "Perelesoq" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "UserName", "PasswordHashed", "Role" },
                values: new object[,]
                {
                    { "enhisir", "baPSLPhLKEISy/ig8xqmMQ==;5b3EjA2COq5gozxHPNiRhjWGtvdaXch7GiemoaN0NJQ=", 0 },
                    { "nikoimam", "lVneYZ/0x5AQox1i/MEP7w==;Ay51S2b0wrAk2WYCHAt2SVneyHAknRYW1dj/QwSms+4=", 0 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "DeveloperId", "PublisherId", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 1, 1, "Cyberpunk 2077", 0 },
                    { 2, 2, 2, "Metal Gear Solid", 0 },
                    { 3, 3, 4, "Halo: Combat Evolved", 0 },
                    { 4, 5, 5, "Torn Away", 0 }
                });

            migrationBuilder.InsertData(
                table: "GameNotes",
                columns: new[] { "GameId", "PlayerId", "Completed", "Favourite", "PlayLater" },
                values: new object[,]
                {
                    { 1, "enhisir", true, true, true },
                    { 3, "enhisir", false, false, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameNotes",
                keyColumns: new[] { "GameId", "PlayerId" },
                keyValues: new object[] { 1, "enhisir" });

            migrationBuilder.DeleteData(
                table: "GameNotes",
                keyColumns: new[] { "GameId", "PlayerId" },
                keyValues: new object[] { 3, "enhisir" });

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "UserName",
                keyValue: "nikoimam");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "UserName",
                keyValue: "enhisir");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
