using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class ValidationToken_Finalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Pessoas_ConjugeId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Pessoas_MaeId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Pessoas_PaiId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Pessoas_PessoaId",
                table: "ValidationTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId",
                table: "ValidationTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Pessoas_ConjugeId",
                table: "Pessoas",
                column: "ConjugeId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Pessoas_MaeId",
                table: "Pessoas",
                column: "MaeId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Pessoas_PaiId",
                table: "Pessoas",
                column: "PaiId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId",
                table: "Pessoas",
                column: "CriadorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Pessoas_PessoaId",
                table: "ValidationTokens",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId",
                table: "ValidationTokens",
                column: "GeradoPorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Pessoas_ConjugeId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Pessoas_MaeId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Pessoas_PaiId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Pessoas_PessoaId",
                table: "ValidationTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId",
                table: "ValidationTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Pessoas_ConjugeId",
                table: "Pessoas",
                column: "ConjugeId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Pessoas_MaeId",
                table: "Pessoas",
                column: "MaeId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Pessoas_PaiId",
                table: "Pessoas",
                column: "PaiId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId",
                table: "Pessoas",
                column: "CriadorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Pessoas_PessoaId",
                table: "ValidationTokens",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId",
                table: "ValidationTokens",
                column: "GeradoPorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
