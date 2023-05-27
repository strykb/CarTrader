using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Data.Migrations
{
    public partial class addCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoldAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Sold = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Canceled = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b43310d-279e-4ad9-9396-3b886134810a", "AQAAAAEAACcQAAAAEGGn7d69W+C3vI1LemaY3j+WQ5AAbx9dnqoHzMhKpZ+4zoKgNszU/F1Ry3m+bCg4Pw==", "df1b4361-aee9-43c2-a6ed-c023de450579" });

            migrationBuilder.CreateIndex(
                name: "IX_Car_UserId",
                table: "Car",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f05f880-4df8-4374-a4d8-ebb68f04bfde", "AQAAAAEAACcQAAAAEPymDp8YrFJkGcQtGNo01F/CvDw0IIb/uqy6howvjt5mAgmn33tcxQEildrfuz4vhQ==", "4995d965-3c84-46d2-87e3-cc876e095813" });
        }
    }
}
