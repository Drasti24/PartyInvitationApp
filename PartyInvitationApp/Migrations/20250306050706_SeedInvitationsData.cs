using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyInvitationApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedInvitationsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteCount",
                table: "Parties");

            migrationBuilder.InsertData(
                table: "Parties",
                columns: new[] { "Id", "Date", "Description", "Location" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Year's Eve Blast!!", "Time Square, NY" },
                    { 2, new DateTime(2022, 10, 30, 16, 43, 12, 0, DateTimeKind.Unspecified), "Drinks at Moe's Bar", "Moe's bar, Springfield" },
                    { 3, new DateTime(2022, 10, 20, 16, 43, 12, 0, DateTimeKind.Unspecified), "Thanksgiving Gathering", "Springfield" }
                });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "GuestEmail", "GuestName", "PartyId", "Status" },
                values: new object[,]
                {
                    { 1, "pmadziak@conestogac.on.ca", "Bob Jones", 1, 1 },
                    { 2, "peter.madziak@gmail.com", "Sally Smith", 1, 1 },
                    { 3, "bart.simpson@gmail.com", "Bart Simpson", 3, 1 },
                    { 4, "lisa.simpson@gmail.com", "Lisa Simpson", 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Invitations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Parties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "InviteCount",
                table: "Parties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
