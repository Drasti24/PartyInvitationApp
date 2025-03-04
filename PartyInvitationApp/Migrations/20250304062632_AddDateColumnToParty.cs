using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyInvitationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDateColumnToParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Parties",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "InviteCount",
                table: "Parties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAttending",
                table: "Invitations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteCount",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "IsAttending",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Parties",
                newName: "EventDate");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Invitations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
