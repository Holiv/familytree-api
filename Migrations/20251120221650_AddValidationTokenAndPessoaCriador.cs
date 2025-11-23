using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationTokenAndPessoaCriador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "ValidationTokens");

            migrationBuilder.RenameColumn(
                name: "Usado",
                table: "ValidationTokens",
                newName: "Utilizado");

            migrationBuilder.RenameColumn(
                name: "ExpiraEm",
                table: "ValidationTokens",
                newName: "DataCriacao");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracao",
                table: "ValidationTokens",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GeradoPorUsuarioId1",
                table: "ValidationTokens",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ValidationTokens_GeradoPorUsuarioId1",
                table: "ValidationTokens",
                column: "GeradoPorUsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId1",
                table: "ValidationTokens",
                column: "GeradoPorUsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValidationTokens_Usuarios_GeradoPorUsuarioId1",
                table: "ValidationTokens");

            migrationBuilder.DropIndex(
                name: "IX_ValidationTokens_GeradoPorUsuarioId1",
                table: "ValidationTokens");

            migrationBuilder.DropColumn(
                name: "DataExpiracao",
                table: "ValidationTokens");

            migrationBuilder.DropColumn(
                name: "GeradoPorUsuarioId1",
                table: "ValidationTokens");

            migrationBuilder.RenameColumn(
                name: "Utilizado",
                table: "ValidationTokens",
                newName: "Usado");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "ValidationTokens",
                newName: "ExpiraEm");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "ValidationTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
