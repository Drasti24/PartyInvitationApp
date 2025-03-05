using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models  
{
    public class Party
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public List<Invitation> Invitations { get; set; } = new();

        // ✅ Compute the InviteCount dynamically
        [NotMapped]
        public int InviteCount => Invitations?.Count ?? 0;
    }
}
