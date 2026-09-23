using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSuite.Api.Migrations.AppSuite
{
    /// <inheritdoc />
    public partial class updatedEquipModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipModel_EquipCategory_EquipCategoryId",
                table: "EquipModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipModel_EquipSubcategory_EquipSubcategoryId",
                table: "EquipModel");

            migrationBuilder.AlterColumn<bool>(
                name: "Serialized",
                table: "EquipModel",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "LotTracked",
                table: "EquipModel",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "EquipSubcategoryId",
                table: "EquipModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EquipCategoryId",
                table: "EquipModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipModel_EquipCategory_EquipCategoryId",
                table: "EquipModel",
                column: "EquipCategoryId",
                principalTable: "EquipCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipModel_EquipSubcategory_EquipSubcategoryId",
                table: "EquipModel",
                column: "EquipSubcategoryId",
                principalTable: "EquipSubcategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipModel_EquipCategory_EquipCategoryId",
                table: "EquipModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipModel_EquipSubcategory_EquipSubcategoryId",
                table: "EquipModel");

            migrationBuilder.AlterColumn<bool>(
                name: "Serialized",
                table: "EquipModel",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "LotTracked",
                table: "EquipModel",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EquipSubcategoryId",
                table: "EquipModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EquipCategoryId",
                table: "EquipModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipModel_EquipCategory_EquipCategoryId",
                table: "EquipModel",
                column: "EquipCategoryId",
                principalTable: "EquipCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipModel_EquipSubcategory_EquipSubcategoryId",
                table: "EquipModel",
                column: "EquipSubcategoryId",
                principalTable: "EquipSubcategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
