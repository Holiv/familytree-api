using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTree.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationTokenAndPessoaCriador2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_UsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_CriadorUsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_UsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "CriadorUsuarioId1",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "DataFalescimento",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Pessoas");

            migrationBuilder.AlterColumn<int>(
                name: "CriadorUsuarioId",
                table: "Pessoas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CriadorUsuarioId",
                table: "Pessoas",
                column: "CriadorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_UsuarioId",
                table: "Pessoas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId",
                table: "Pessoas",
                column: "CriadorUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_UsuarioId",
                table: "Pessoas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_UsuarioId",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_CriadorUsuarioId",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_UsuarioId",
                table: "Pessoas");

            migrationBuilder.AlterColumn<int>(
                name: "CriadorUsuarioId",
                table: "Pessoas",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CriadorUsuarioId1",
                table: "Pessoas",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFalescimento",
                table: "Pessoas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Pessoas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CriadorUsuarioId1",
                table: "Pessoas",
                column: "CriadorUsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_UsuarioId1",
                table: "Pessoas",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_CriadorUsuarioId1",
                table: "Pessoas",
                column: "CriadorUsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_UsuarioId1",
                table: "Pessoas",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
