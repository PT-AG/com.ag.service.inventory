using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Ambassador.Service.Inventory.Lib.Migrations
{
    public partial class addTableAvalCustomsOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBC",
                table: "GarmentLeftoverWarehouseExpenditureAvals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GarmentLeftoverWarehouseAvalsCustomsOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    BCNo = table.Column<string>(maxLength: 255, nullable: true),
                    BCDate = table.Column<DateTimeOffset>(nullable: false),
                    BCType = table.Column<string>(maxLength: 20, nullable: true),
                    IsSubcon = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentLeftoverWarehouseAvalsCustomsOuts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentLeftoverWarehouseAvalsCustomsOutItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CustomsOutId = table.Column<int>(nullable: false),
                    AvalExpenditureId = table.Column<int>(nullable: false),
                    AvalExpenditureNo = table.Column<string>(maxLength: 20, nullable: true),
                    ProductName = table.Column<string>(maxLength: 20, nullable: true),
                    UomId = table.Column<long>(nullable: false),
                    UomUnit = table.Column<string>(maxLength: 10, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentLeftoverWarehouseAvalsCustomsOutItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarmentLeftoverWarehouseAvalsCustomsOutItems_GarmentLeftoverWarehouseAvalsCustomsOuts_CustomsOutId",
                        column: x => x.CustomsOutId,
                        principalTable: "GarmentLeftoverWarehouseAvalsCustomsOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentLeftoverWarehouseAvalsCustomsOutItems_CustomsOutId",
                table: "GarmentLeftoverWarehouseAvalsCustomsOutItems",
                column: "CustomsOutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentLeftoverWarehouseAvalsCustomsOutItems");

            migrationBuilder.DropTable(
                name: "GarmentLeftoverWarehouseAvalsCustomsOuts");

            migrationBuilder.DropColumn(
                name: "IsBC",
                table: "GarmentLeftoverWarehouseExpenditureAvals");
        }
    }
}
