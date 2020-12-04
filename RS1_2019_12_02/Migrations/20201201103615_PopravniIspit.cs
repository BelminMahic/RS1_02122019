using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_2019_12_02.Migrations
{
    public partial class PopravniIspit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PopravniIspit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdjeljenjeId = table.Column<int>(type: "int", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false),
                    SkolaId = table.Column<int>(type: "int", nullable: false),
                    SkolskaGodinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Skola_SkolaId",
                        column: x => x.SkolaId,
                        principalTable: "Skola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_SkolskaGodina_SkolskaGodinaId",
                        column: x => x.SkolskaGodinaId,
                        principalTable: "SkolskaGodina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_OdjeljenjeId",
                table: "PopravniIspit",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_PredmetId",
                table: "PopravniIspit",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_SkolaId",
                table: "PopravniIspit",
                column: "SkolaId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_SkolskaGodinaId",
                table: "PopravniIspit",
                column: "SkolskaGodinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopravniIspit");
        }
    }
}
