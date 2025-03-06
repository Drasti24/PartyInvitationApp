//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025 

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models  
{
    // Represents a party event that users can create and manage
    public class Party
    {
        // Unique identifier for the party
        public int Id { get; set; }

        // A brief description of the party 
        [Required]
        public string Description { get; set; } = string.Empty;

        // Date and time of the party
        [Required]
        public DateTime Date { get; set; }

        // Location of the party
        [Required]
        public string Location { get; set; } = string.Empty;

        // Navigation property: A list of invitations associated with this party
        public List<Invitation> Invitations { get; set; } = new();

        // Compute the InviteCount dynamically (Not stored in the database)
        [NotMapped]     // Excludes this property from the database schema
        public int InviteCount => Invitations?.Count ?? 0;
    }
}
