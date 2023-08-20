using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kdan.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeNumber",
                table: "members",
                type: "int",
                maxLength: 36,
                nullable: false,
                comment: "員工編號",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldComment: "員工編號")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_bin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeNumber",
                table: "members",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                comment: "員工編號",
                collation: "utf8mb4_bin",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 36,
                oldComment: "員工編號")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
