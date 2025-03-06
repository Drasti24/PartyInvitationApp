//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025 

using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Models;

namespace PartyInvitationApp.Data
{
    // This class represents the database context for the application
    // It is responsible for interacting with the database using Entity Framework Core
    public class ApplicationDbContext : DbContext
    {
        // Constructor: Initializes the database context with the provided options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
      
        // Represents the "Parties" table in the database
        public DbSet<Party> Parties { get; set; }

        // Represents the "Invitations" table in the database
        public DbSet<Invitation> Invitations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Party Data (Events)
            modelBuilder.Entity<Party>().HasData(
                new Party
                {
                    Id = 1,
                    Description = "New Year's Eve Blast!!",
                    Date = new DateTime(2022, 12, 31, 0, 0, 0),
                    Location = "Time Square, NY"
                },
                new Party
                {
                    Id = 2,
                    Description = "Drinks at Moe's Bar",
                    Date = new DateTime(2022, 10, 30, 16, 43, 12),
                    Location = "Moe's bar, Springfield"
                },
                new Party
                {
                    Id = 3,
                    Description = "Thanksgiving Gathering",
                    Date = new DateTime(2022, 10, 20, 16, 43, 12),
                    Location = "Springfield"
                }
            );

            modelBuilder.Entity<Invitation>().HasData(
                new Invitation
                {
                    Id = 1,
                    GuestName = "Bob Jones",
                    GuestEmail = "pmadziak@conestogac.on.ca",
                    Status = InvitationStatus.InviteSent,
                    PartyId = 1
                },
                new Invitation
                {
                    Id = 2,
                    GuestName = "Sally Smith",
                    GuestEmail = "peter.madziak@gmail.com",
                    Status = InvitationStatus.InviteSent,
                    PartyId = 1
                },

                new Invitation
                {
                    Id = 3,
                    GuestName = "Bart Simpson",
                    GuestEmail = "bart.simpson@gmail.com",
                    Status = InvitationStatus.InviteSent,
                    PartyId = 3
                },
                new Invitation
                {
                    Id = 4,
                    GuestName = "Lisa Simpson",
                    GuestEmail = "lisa.simpson@gmail.com",
                    Status = InvitationStatus.InviteSent,
                    PartyId = 3
                }
            );
        }

    }
}
