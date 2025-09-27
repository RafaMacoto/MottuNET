using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuNET.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarTabelasNomeadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_moto_tb_ala_AlaId",
                table: "tb_moto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_moto",
                table: "tb_moto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_ala",
                table: "tb_ala");

            migrationBuilder.RenameTable(
                name: "tb_usuario",
                newName: "tb_usuarioMottu");

            migrationBuilder.RenameTable(
                name: "tb_moto",
                newName: "tb_motoMottu");

            migrationBuilder.RenameTable(
                name: "tb_ala",
                newName: "tb_alaMottu");

            migrationBuilder.RenameIndex(
                name: "IX_tb_moto_AlaId",
                table: "tb_motoMottu",
                newName: "IX_tb_motoMottu_AlaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_usuarioMottu",
                table: "tb_usuarioMottu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_motoMottu",
                table: "tb_motoMottu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_alaMottu",
                table: "tb_alaMottu",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_motoMottu_tb_alaMottu_AlaId",
                table: "tb_motoMottu",
                column: "AlaId",
                principalTable: "tb_alaMottu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_motoMottu_tb_alaMottu_AlaId",
                table: "tb_motoMottu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_usuarioMottu",
                table: "tb_usuarioMottu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_motoMottu",
                table: "tb_motoMottu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_alaMottu",
                table: "tb_alaMottu");

            migrationBuilder.RenameTable(
                name: "tb_usuarioMottu",
                newName: "tb_usuario");

            migrationBuilder.RenameTable(
                name: "tb_motoMottu",
                newName: "tb_moto");

            migrationBuilder.RenameTable(
                name: "tb_alaMottu",
                newName: "tb_ala");

            migrationBuilder.RenameIndex(
                name: "IX_tb_motoMottu_AlaId",
                table: "tb_moto",
                newName: "IX_tb_moto_AlaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_usuario",
                table: "tb_usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_moto",
                table: "tb_moto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_ala",
                table: "tb_ala",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_moto_tb_ala_AlaId",
                table: "tb_moto",
                column: "AlaId",
                principalTable: "tb_ala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
