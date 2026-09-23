using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSuite.Api.Migrations.AppSuite
{
    /// <inheritdoc />
    public partial class initialAppSuiteDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipManuf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipManuf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipSubcategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EquipCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipSubcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipSubcategory_EquipCategory_EquipCategoryId",
                        column: x => x.EquipCategoryId,
                        principalTable: "EquipCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    ModelNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EquipCategoryId = table.Column<int>(type: "int", nullable: false),
                    EquipSubcategoryId = table.Column<int>(type: "int", nullable: false),
                    LotTracked = table.Column<bool>(type: "bit", nullable: false),
                    Serialized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipModel_EquipCategory_EquipCategoryId",
                        column: x => x.EquipCategoryId,
                        principalTable: "EquipCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipModel_EquipManuf_MakeId",
                        column: x => x.MakeId,
                        principalTable: "EquipManuf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipModel_EquipSubcategory_EquipSubcategoryId",
                        column: x => x.EquipSubcategoryId,
                        principalTable: "EquipSubcategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    ModelNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LotNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SerialNum = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EquipCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipCategory_EquipCategoryId",
                        column: x => x.EquipCategoryId,
                        principalTable: "EquipCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipManuf_MakeId",
                        column: x => x.MakeId,
                        principalTable: "EquipManuf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "EquipModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipCategoryId",
                table: "Equipment",
                column: "EquipCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_MakeId",
                table: "Equipment",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_ModelId",
                table: "Equipment",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipModel_EquipCategoryId",
                table: "EquipModel",
                column: "EquipCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipModel_EquipSubcategoryId",
                table: "EquipModel",
                column: "EquipSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipModel_MakeId",
                table: "EquipModel",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipSubcategory_EquipCategoryId",
                table: "EquipSubcategory",
                column: "EquipCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipModel");

            migrationBuilder.DropTable(
                name: "EquipManuf");

            migrationBuilder.DropTable(
                name: "EquipSubcategory");

            migrationBuilder.DropTable(
                name: "EquipCategory");
        }
    }
}
