using Microsoft.EntityFrameworkCore.Migrations;

namespace Aula_03.Migrations
{
    public partial class add_numero_de_paginas_livro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroPagina",
                table: "TBLivro",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroPagina",
                table: "TBLivro");
        }
    }
}
