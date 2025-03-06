//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025 

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    // Enum representing the different statuses of an invitation
    public enum InvitationStatus
    {
        InviteNotSent,     // Default status - invitation not yet sent
        InviteSent,        // Invitation has been sent but not responded to
        RespondedYes,      // Guest has confirmed attendance
        RespondedNo        // Guest has declined the invitation
    }

    // Represents an invitation sent to a guest for a party
    public class Invitation
    {
        // Unique identifier for the invitation
        public int Id { get; set; }

        // Name of the guest invited (Required field)
        [Required]
        public string GuestName { get; set; } = string.Empty;

        // Email address of the guest (Required and must be a valid email format)
        [Required, EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;

        // Current status of the invitation (Defaults to InviteNotSent)
        [Required]
        public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;

        // Foreign key: Links this invitation to a specific party
        public int PartyId { get; set; }

        // Navigation property: References the associated Party object
        public Party Party { get; set; } = null!;
    }
}
