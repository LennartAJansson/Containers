#nullable disable

namespace BuildVersion.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CleanUpChangeTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "Changed",
                table: "BuildVersions");

            _ = migrationBuilder.DropColumn(
                name: "Created",
                table: "BuildVersions");

            _ = migrationBuilder.DropColumn(
                name: "Changed",
                table: "Binaries");

            _ = migrationBuilder.DropColumn(
                name: "Created",
                table: "Binaries");

            _ = migrationBuilder.UpdateData(
                table: "LogEntry",
                keyColumn: "JsonBefore",
                keyValue: null,
                column: "JsonBefore",
                value: "");

            _ = migrationBuilder.AlterColumn<string>(
                name: "JsonBefore",
                table: "LogEntry",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.UpdateData(
                table: "LogEntry",
                keyColumn: "JsonAfter",
                keyValue: null,
                column: "JsonAfter",
                value: "");

            _ = migrationBuilder.AlterColumn<string>(
                name: "JsonAfter",
                table: "LogEntry",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.UpdateData(
                table: "BuildVersions",
                keyColumn: "SemanticVersionPre",
                keyValue: null,
                column: "SemanticVersionPre",
                value: "");

            _ = migrationBuilder.AlterColumn<string>(
                name: "SemanticVersionPre",
                table: "BuildVersions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.UpdateData(
                table: "Binaries",
                keyColumn: "ProjectFile",
                keyValue: null,
                column: "ProjectFile",
                value: "");

            _ = migrationBuilder.AlterColumn<string>(
                name: "ProjectFile",
                table: "Binaries",
                type: "varchar(95)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(95)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropIndex(
                name: "IX_Binaries_BuildVersionId",
                table: "Binaries");

            _ = migrationBuilder.AlterColumn<string>(
                name: "JsonBefore",
                table: "LogEntry",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.AlterColumn<string>(
                name: "JsonAfter",
                table: "LogEntry",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.AlterColumn<string>(
                name: "SemanticVersionPre",
                table: "BuildVersions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "BuildVersions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            _ = migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "BuildVersions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            _ = migrationBuilder.AlterColumn<string>(
                name: "ProjectFile",
                table: "Binaries",
                type: "varchar(95)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(95)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            _ = migrationBuilder.AddColumn<DateTime>(
                name: "Changed",
                table: "Binaries",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            _ = migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Binaries",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            _ = migrationBuilder.CreateIndex(
                name: "IX_Binaries_BuildVersionId",
                table: "Binaries",
                column: "BuildVersionId");
        }
    }
}
