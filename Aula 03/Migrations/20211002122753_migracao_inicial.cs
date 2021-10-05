using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aula_03.Migrations
{
    public partial class migracao_inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBClassificacaoLivro",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classificacao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBClassificacaoLivro", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBEditora",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEditora = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBEditora", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBLivro",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcessoOnline = table.Column<bool>(type: "bit", nullable: false),
                    EditoraID = table.Column<int>(type: "int", nullable: false),
                    ClassificacaoLivroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLivro", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TBLivro_TBClassificacaoLivro_ClassificacaoLivroID",
                        column: x => x.ClassificacaoLivroID,
                        principalTable: "TBClassificacaoLivro",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBLivro_TBEditora_EditoraID",
                        column: x => x.EditoraID,
                        principalTable: "TBEditora",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBLivro_ClassificacaoLivroID",
                table: "TBLivro",
                column: "ClassificacaoLivroID");

            migrationBuilder.CreateIndex(
                name: "IX_TBLivro_EditoraID",
                table: "TBLivro",
                column: "EditoraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBLivro");

            migrationBuilder.DropTable(
                name: "TBClassificacaoLivro");

            migrationBuilder.DropTable(
                name: "TBEditora");
        }
    }
}
