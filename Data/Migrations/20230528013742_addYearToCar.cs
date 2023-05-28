using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTrader.Data.Migrations
{
    public partial class addYearToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 2023);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fb10fa8-abd4-453d-9d41-8f02bf492a33", "AQAAAAEAACcQAAAAEKweejcUCKbcNK8WpF3QsIqzntJktwJv5FwjAhEpHXyw/3vw9lo6uhadX3mllOimmw==", "0def6b66-86d1-4f9c-85ae-f0d0444ea5a5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Car");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b43310d-279e-4ad9-9396-3b886134810a", "AQAAAAEAACcQAAAAEGGn7d69W+C3vI1LemaY3j+WQ5AAbx9dnqoHzMhKpZ+4zoKgNszU/F1Ry3m+bCg4Pw==", "df1b4361-aee9-43c2-a6ed-c023de450579" });
        }
    }
}
