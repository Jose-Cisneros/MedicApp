using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class consultaModi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Pacientes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Consultas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Consultas");
        }
    }
}
