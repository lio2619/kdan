using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kdan.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 36, nullable: false, comment: "流水編號")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeNumber = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, comment: "員工編號", collation: "utf8mb4_bin")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClockIn = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "上班打卡時間"),
                    ClockOut = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "下班打卡時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.Id);
                },
                comment: "員工打卡時間")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_bin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "members");
        }
    }
}
