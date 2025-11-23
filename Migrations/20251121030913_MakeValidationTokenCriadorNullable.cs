using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class MakeValidationTokenCriadorNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId1",
                table: "ValidationTokens");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "GeradoPorUsuarioId1",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "GeradoPorUsuarioId",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId1",
                table: "ValidationTokens",
                column: "GeradoPorUsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId1",
                table: "ValidationTokens");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GeradoPorUsuarioId1",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GeradoPorUsuarioId",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId1",
                table: "ValidationTokens",
                column: "GeradoPorUsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
