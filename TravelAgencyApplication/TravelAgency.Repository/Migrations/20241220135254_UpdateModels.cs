using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationInPackage_Accommodations_AccommodationId",
                table: "AccommodationInPackage");

            migrationBuilder.DropColumn(
                name: "AccomodationId",
                table: "AccommodationInPackage");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccommodationId",
                table: "AccommodationInPackage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationInPackage_Accommodations_AccommodationId",
                table: "AccommodationInPackage",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationInPackage_Accommodations_AccommodationId",
                table: "AccommodationInPackage");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccommodationId",
                table: "AccommodationInPackage",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AccomodationId",
                table: "AccommodationInPackage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationInPackage_Accommodations_AccommodationId",
                table: "AccommodationInPackage",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id");
        }
    }
}
