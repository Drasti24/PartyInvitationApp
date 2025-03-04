using System;
using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; }

        public bool IsAttending { get; set; }

        // Foreign key reference to Party
        public int PartyId { get; set; }
        public Party Party { get; set; }
    }
}
