using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuNET.Migrations
{
    /// <inheritdoc />
    public partial class Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_ala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_moto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Modelo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Posicao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Problema = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Placa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AlaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_moto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_moto_tb_ala_AlaId",
                        column: x => x.AlaId,
                        principalTable: "tb_ala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_moto_AlaId",
                table: "tb_moto",
                column: "AlaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_moto");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_ala");
        }
    }
}
