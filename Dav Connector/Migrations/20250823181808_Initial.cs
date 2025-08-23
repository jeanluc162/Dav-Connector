using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dav_Connector.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountTypeId = table.Column<Guid>(nullable: true),
                    EncryptedPassword = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SyncTypeId = table.Column<Guid>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_SyncTypes_SyncTypeId",
                        column: x => x.SyncTypeId,
                        principalTable: "SyncTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SyncTypeId",
                table: "Accounts",
                column: "SyncTypeId");

            migrationBuilder.Sql($"INSERT INTO SyncTypes (Id, Name) VALUES (X'{BitConverter.ToString(Guid.Parse("24dd7f72-335a-48ec-8ce7-7204bb3359b4").ToByteArray()).Replace("-", "")}','Remote to Local'), (X'{BitConverter.ToString(Guid.Parse("5023eecd-324a-4112-899b-1ec3f4bf7c53").ToByteArray()).Replace("-", "")}','Local to Remote'), (X'{BitConverter.ToString(Guid.Parse("2b52f274-5f3b-4c8d-82e0-20ef84f492fb").ToByteArray()).Replace("-", "")}','Both Ways')");
            migrationBuilder.Sql($"INSERT INTO AccountTypes (Id, Name) VALUES (X'{BitConverter.ToString(Guid.Parse("31585b9b-7549-4d77-828f-670d617c5fdd").ToByteArray()).Replace("-", "")}','CardDav')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "SyncTypes");
        }
    }
}
