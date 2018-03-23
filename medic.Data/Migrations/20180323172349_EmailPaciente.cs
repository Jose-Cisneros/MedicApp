using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class EmailPaciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pacientes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pacientes");
        }
    }
}
