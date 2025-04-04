using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Futures.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FutureContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Asset = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContractType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FutureContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceDifferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentFuturesContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    NextFuturesContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IntervalType = table.Column<string>(type: "text", nullable: false),
                    Difference = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDifferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceDifferences_FutureContracts_CurrentFuturesContractId",
                        column: x => x.CurrentFuturesContractId,
                        principalTable: "FutureContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceDifferences_FutureContracts_NextFuturesContractId",
                        column: x => x.NextFuturesContractId,
                        principalTable: "FutureContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PricePoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FutureContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricePoints_FutureContracts_FutureContractId",
                        column: x => x.FutureContractId,
                        principalTable: "FutureContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceDifferences_CurrentFuturesContractId",
                table: "PriceDifferences",
                column: "CurrentFuturesContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceDifferences_NextFuturesContractId",
                table: "PriceDifferences",
                column: "NextFuturesContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PricePoints_FutureContractId",
                table: "PricePoints",
                column: "FutureContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceDifferences");

            migrationBuilder.DropTable(
                name: "PricePoints");

            migrationBuilder.DropTable(
                name: "FutureContracts");
        }
    }
}
