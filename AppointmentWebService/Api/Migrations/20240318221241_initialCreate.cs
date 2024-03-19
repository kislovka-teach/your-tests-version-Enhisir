using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHashed = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    SpecializationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                    table.ForeignKey(
                        name: "FK_Users_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DoctorUserName = table.Column<string>(type: "text", nullable: false),
                    PatientUserName = table.Column<string>(type: "text", nullable: false),
                    PatientUserName1 = table.Column<string>(type: "text", nullable: true),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    Finding = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Users_DoctorUserName",
                        column: x => x.DoctorUserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Users_PatientUserName",
                        column: x => x.PatientUserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Users_PatientUserName1",
                        column: x => x.PatientUserName1,
                        principalTable: "Users",
                        principalColumn: "UserName");
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Handsome master" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Discriminator", "Name", "PasswordHashed", "Role", "Surname" },
                values: new object[,]
                {
                    { "enhisir", "Patient", "m", "kMauyjlmyQ21EU74zgh5Xw==;RUcTLejCWX9IQrnQE8Dwbc9VMzeOB5QwKNnv8EJP3+A=", 0, "s" },
                    { "nikoimam", "Patient", "n", "OkabuTLPhHUMoauV+EHsrA==;om6GF8jrbZz87b1c3WEqRRCkoni/XJOEJLSeFn2ng6o=", 0, "i" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Discriminator", "Name", "PasswordHashed", "Role", "SpecializationId", "Surname" },
                values: new object[] { "bold", "Doctor", "Johnny", "BkzkmI4A3hFvbaqygoUp3A==;nb5zeL+pk5DcHH1ZHeRAs8uQRhS0m3Lej76miizLckw=", 0, 1, "Sins" });

            migrationBuilder.InsertData(
                table: "Visits",
                columns: new[] { "Id", "Date", "DoctorUserName", "Finding", "IsSuccessful", "PatientUserName", "PatientUserName1" },
                values: new object[] { 1, new DateTime(2024, 3, 18, 21, 0, 0, 0, DateTimeKind.Utc), "bold", "pomer...", true, "enhisir", null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecializationId",
                table: "Users",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorUserName",
                table: "Visits",
                column: "DoctorUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientUserName",
                table: "Visits",
                column: "PatientUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientUserName1",
                table: "Visits",
                column: "PatientUserName1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
