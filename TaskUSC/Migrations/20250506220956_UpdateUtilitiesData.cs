using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskUSC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUtilitiesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectricPowerId",
                table: "UtilitiesData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeatCarrierId",
                table: "UtilitiesData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThermalEnergyId",
                table: "UtilitiesData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "СoldWaterId",
                table: "UtilitiesData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ColdWater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Volume = table.Column<decimal>(type: "TEXT", nullable: false),
                    MeteringDeviceReadings = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColdWater", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectricPower",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    VolumeDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    CostNight = table.Column<decimal>(type: "TEXT", nullable: false),
                    VolumeNight = table.Column<decimal>(type: "TEXT", nullable: false),
                    MeteringDeviceReadingsDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    MeteringDeviceReadingsNight = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricPower", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeatCarrier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Volume = table.Column<decimal>(type: "TEXT", nullable: false),
                    MeteringDeviceReadings = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatCarrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThermalEnergy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Volume = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThermalEnergy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilitiesData_СoldWaterId",
                table: "UtilitiesData",
                column: "СoldWaterId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilitiesData_ElectricPowerId",
                table: "UtilitiesData",
                column: "ElectricPowerId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilitiesData_HeatCarrierId",
                table: "UtilitiesData",
                column: "HeatCarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilitiesData_ThermalEnergyId",
                table: "UtilitiesData",
                column: "ThermalEnergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilitiesData_ColdWater_СoldWaterId",
                table: "UtilitiesData",
                column: "СoldWaterId",
                principalTable: "ColdWater",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilitiesData_ElectricPower_ElectricPowerId",
                table: "UtilitiesData",
                column: "ElectricPowerId",
                principalTable: "ElectricPower",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilitiesData_HeatCarrier_HeatCarrierId",
                table: "UtilitiesData",
                column: "HeatCarrierId",
                principalTable: "HeatCarrier",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilitiesData_ThermalEnergy_ThermalEnergyId",
                table: "UtilitiesData",
                column: "ThermalEnergyId",
                principalTable: "ThermalEnergy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilitiesData_ColdWater_СoldWaterId",
                table: "UtilitiesData");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilitiesData_ElectricPower_ElectricPowerId",
                table: "UtilitiesData");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilitiesData_HeatCarrier_HeatCarrierId",
                table: "UtilitiesData");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilitiesData_ThermalEnergy_ThermalEnergyId",
                table: "UtilitiesData");

            migrationBuilder.DropTable(
                name: "ColdWater");

            migrationBuilder.DropTable(
                name: "ElectricPower");

            migrationBuilder.DropTable(
                name: "HeatCarrier");

            migrationBuilder.DropTable(
                name: "ThermalEnergy");

            migrationBuilder.DropIndex(
                name: "IX_UtilitiesData_СoldWaterId",
                table: "UtilitiesData");

            migrationBuilder.DropIndex(
                name: "IX_UtilitiesData_ElectricPowerId",
                table: "UtilitiesData");

            migrationBuilder.DropIndex(
                name: "IX_UtilitiesData_HeatCarrierId",
                table: "UtilitiesData");

            migrationBuilder.DropIndex(
                name: "IX_UtilitiesData_ThermalEnergyId",
                table: "UtilitiesData");

            migrationBuilder.DropColumn(
                name: "ElectricPowerId",
                table: "UtilitiesData");

            migrationBuilder.DropColumn(
                name: "HeatCarrierId",
                table: "UtilitiesData");

            migrationBuilder.DropColumn(
                name: "ThermalEnergyId",
                table: "UtilitiesData");

            migrationBuilder.DropColumn(
                name: "СoldWaterId",
                table: "UtilitiesData");
        }
    }
}
