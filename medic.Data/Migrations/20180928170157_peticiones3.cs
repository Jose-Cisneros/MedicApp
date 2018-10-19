using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class peticiones3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeticionPacienteAMedicos",
                columns: table => new
                {
                    PeticionPacienteAMedicoID = table.Column<string>(nullable: false),
                    MedicoID = table.Column<string>(nullable: true),
                    PacienteID = table.Column<string>(nullable: true),
                    visto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeticionPacienteAMedicos", x => x.PeticionPacienteAMedicoID);
                    table.ForeignKey(
                        name: "FK_PeticionPacienteAMedicos_Medicos_MedicoID",
                        column: x => x.MedicoID,
                        principalTable: "Medicos",
                        principalColumn: "MedicoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeticionPacienteAMedicos_Pacientes_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeticionPacienteAMedicos_MedicoID",
                table: "PeticionPacienteAMedicos",
                column: "MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionPacienteAMedicos_PacienteID",
                table: "PeticionPacienteAMedicos",
                column: "PacienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeticionPacienteAMedicos");
        }
    }
}
