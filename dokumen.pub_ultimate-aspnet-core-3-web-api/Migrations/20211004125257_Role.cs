using Microsoft.EntityFrameworkCore.Migrations;

namespace dokumen.pub_ultimate_aspnet_core_3_web_api.Migrations
{
    public partial class Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ImportantDegree", "Name", "NormalizedName" },
                values: new object[] { "c93bcb1e-eaa6-4802-ae07-0d54d7c68594", "173b9bc8-d5ba-4fcb-8b40-b7b9c648b99e", 1, "Manger", "MANGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ImportantDegree", "Name", "NormalizedName" },
                values: new object[] { "12751ba4-c829-4d7f-885e-ae28c2b50a4e", "850d3681-2a99-4f3f-b224-0937e6967e5b", 2, "Adminstrator", "ADMINSTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12751ba4-c829-4d7f-885e-ae28c2b50a4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c93bcb1e-eaa6-4802-ae07-0d54d7c68594");
        }
    }
}
