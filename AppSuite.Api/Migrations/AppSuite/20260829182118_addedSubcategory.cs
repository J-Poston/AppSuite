using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSuite.Api.Migrations.AppSuite
{
    /// <inheritdoc />
    public partial class addedSubcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipCategory_EquipCategoryId",
                table: "Equipment");

            migrationBuilder.AlterColumn<int>(
                name: "EquipCategoryId",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EquipSubcategoryId",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipSubcategoryId",
                table: "Equipment",
                column: "EquipSubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipCategory_EquipCategoryId",
                table: "Equipment",
                column: "EquipCategoryId",
                principalTable: "EquipCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipSubcategory_EquipSubcategoryId",
                table: "Equipment",
                column: "EquipSubcategoryId",
                principalTable: "EquipSubcategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipCategory_EquipCategoryId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipSubcategory_EquipSubcategoryId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_EquipSubcategoryId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipSubcategoryId",
                table: "Equipment");

            migrationBuilder.AlterColumn<int>(
                name: "EquipCategoryId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipCategory_EquipCategoryId",
                table: "Equipment",
                column: "EquipCategoryId",
                principalTable: "EquipCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
