using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "devtest.Request",
                columns: table => new
                {
                    Rid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kind = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Rid);
                });

            migrationBuilder.CreateTable(
                name: "devtest.SearchRequest",
                columns: table => new
                {
                    Rid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Search = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SuccessInd = table.Column<string>(type: "char(1)", nullable: false),
                    Hits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequest", x => x.Rid);
                });

            migrationBuilder.CreateTable(
                name: "devtest.SearchTopProducts",
                columns: table => new
                {
                    Rid = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchTopProducts", x => new { x.Rid, x.Order });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "devtest.Request");

            migrationBuilder.DropTable(
                name: "devtest.SearchRequest");

            migrationBuilder.DropTable(
                name: "devtest.SearchTopProducts");
        }
    }
}
