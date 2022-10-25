using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    PictureRelativePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalPersons_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysicalPersonId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_PhysicalPersons_PhysicalPersonId",
                        column: x => x.PhysicalPersonId,
                        principalTable: "PhysicalPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPersonRelations",
                columns: table => new
                {
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    RelatedId = table.Column<int>(type: "int", nullable: false),
                    Relation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPersonRelations", x => new { x.MasterId, x.RelatedId });
                    table.ForeignKey(
                        name: "FK_PhysicalPersonRelations_PhysicalPersons_MasterId",
                        column: x => x.MasterId,
                        principalTable: "PhysicalPersons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhysicalPersonRelations_PhysicalPersons_RelatedId",
                        column: x => x.RelatedId,
                        principalTable: "PhysicalPersons",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tbilisi" },
                    { 2, "Sagarejo" },
                    { 3, "Gurjaani" },
                    { 4, "Tevali" }
                });

            migrationBuilder.InsertData(
                table: "PhysicalPersons",
                columns: new[] { "Id", "CityId", "DateOfBirth", "Gender", "LastName", "Name", "PersonalNumber", "PictureRelativePath" },
                values: new object[] { 1, 1, new DateTime(1996, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Botchorishvili", "Temo", "01011086652", "C://" });

            migrationBuilder.InsertData(
                table: "PhysicalPersons",
                columns: new[] { "Id", "CityId", "DateOfBirth", "Gender", "LastName", "Name", "PersonalNumber", "PictureRelativePath" },
                values: new object[] { 2, 2, new DateTime(1996, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Zibzibadze", "Kote", "01211086652", "C://" });

            migrationBuilder.InsertData(
                table: "PhysicalPersons",
                columns: new[] { "Id", "CityId", "DateOfBirth", "Gender", "LastName", "Name", "PersonalNumber", "PictureRelativePath" },
                values: new object[] { 3, 1, new DateTime(1996, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Egadze", "Soso", "01211086652", "C://" });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Number", "PhysicalPersonId", "Type" },
                values: new object[,]
                {
                    { 1, "551 75 67 76", 1, 0 },
                    { 2, "032 2 75 67 76", 1, 2 },
                    { 3, "551 12 13 76", 2, 0 },
                    { 4, "032 2 33 22 76", 2, 2 },
                    { 5, "551 12 13 57", 3, 0 },
                    { 6, "032 2 64 55 76", 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "PhysicalPersonRelations",
                columns: new[] { "MasterId", "RelatedId", "Relation" },
                values: new object[,]
                {
                    { 1, 2, 2 },
                    { 1, 3, 0 },
                    { 2, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PhysicalPersonId",
                table: "PhoneNumbers",
                column: "PhysicalPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalPersonRelations_RelatedId",
                table: "PhysicalPersonRelations",
                column: "RelatedId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalPersons_CityId",
                table: "PhysicalPersons",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "PhysicalPersonRelations");

            migrationBuilder.DropTable(
                name: "PhysicalPersons");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
