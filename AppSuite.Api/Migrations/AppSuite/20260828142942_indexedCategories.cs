using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSuite.Api.Migrations.AppSuite
{
    /// <inheritdoc />
    public partial class indexedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EquipSubcategory_EquipCategoryId",
                table: "EquipSubcategory");

            migrationBuilder.CreateIndex(
                name: "IX_EquipSubcategory_EquipCategoryId_Name",
                table: "EquipSubcategory",
                columns: new[] { "EquipCategoryId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipCategory_Name",
                table: "EquipCategory",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EquipSubcategory_EquipCategoryId_Name",
                table: "EquipSubcategory");

            migrationBuilder.DropIndex(
                name: "IX_EquipCategory_Name",
                table: "EquipCategory");

            migrationBuilder.CreateIndex(
                name: "IX_EquipSubcategory_EquipCategoryId",
                table: "EquipSubcategory",
                column: "EquipCategoryId");
        }
    }
}
