using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildVersion.Migrations
{
    public partial class semverpre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.RenameColumn(
                name: "SemanticVersionRevision",
                table: "BuildVersions",
                newName: "SemanticVersionPre");

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.RenameColumn(
                name: "SemanticVersionPre",
                table: "BuildVersions",
                newName: "SemanticVersionRevision");
    }
}
