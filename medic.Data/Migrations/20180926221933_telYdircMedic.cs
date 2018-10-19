using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class telYdircMedic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Medicos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Telefono",
                table: "Medicos",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Medicos");
        }
    }
}
