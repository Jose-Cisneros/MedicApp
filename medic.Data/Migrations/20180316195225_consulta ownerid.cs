using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace medic.Data.Migrations
{
    public partial class consultaownerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Consultas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Consultas");
        }
    }
}
