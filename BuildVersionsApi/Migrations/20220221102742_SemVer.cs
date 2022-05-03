using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildVersion.Migrations
{
    public partial class SemVer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<string>(
                name: "SemanticVersionRevision",
                table: "BuildVersions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
                name: "SemanticVersionRevision",
                table: "BuildVersions");
    }
}
