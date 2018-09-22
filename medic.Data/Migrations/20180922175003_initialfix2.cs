using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class initialfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    MedicoID = table.Column<string>(nullable: false),
                    DNI = table.Column<int>(nullable: false),
                    Matricula = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.MedicoID);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteID = table.Column<string>(nullable: false),
                    DNI = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    Telefono = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteID);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    ConsultaID = table.Column<string>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    MedicoID = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    OwnerID = table.Column<string>(nullable: true),
                    PacienteID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.ConsultaID);
                    table.ForeignKey(
                        name: "FK_Consultas_Medicos_MedicoID",
                        column: x => x.MedicoID,
                        principalTable: "Medicos",
                        principalColumn: "MedicoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoID",
                table: "Consultas",
                column: "MedicoID");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteID",
                table: "Consultas",
                column: "PacienteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
