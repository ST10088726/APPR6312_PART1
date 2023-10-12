using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APPR_AZURE_CONNECT.Migrations
{
    /// <inheritdoc />
    public partial class TablesMontDisaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disaster",
                columns: table => new
                {
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAidTypes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disaster", x => x.StartDate);
                });

            migrationBuilder.CreateTable(
                name: "MonetDonation",
                columns: table => new
                {
                    DonatorsName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetDonation", x => x.DonatorsName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disaster");

            migrationBuilder.DropTable(
                name: "MonetDonation");
        }
    }
}
