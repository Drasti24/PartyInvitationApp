using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyInvitationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAttending",
                table: "Invitations",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invitations",
                newName: "IsAttending");
        }
    }
}
