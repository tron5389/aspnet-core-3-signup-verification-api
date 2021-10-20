using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class RentalKPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalKPIs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<string>(nullable: true),
                    ListingId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    MonthlyRent = table.Column<double>(nullable: false),
                    Beds = table.Column<float>(nullable: false),
                    Baths = table.Column<float>(nullable: false),
                    Sqft = table.Column<int>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Lon = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalKPIs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalKPIs");
        }
    }
}
