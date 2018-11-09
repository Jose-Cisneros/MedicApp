using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class sprint22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PacienteNombre",
                table: "PeticionPacienteAMedicos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PacienteNombre",
                table: "PeticionPacienteAMedicos");
        }
    }
}
