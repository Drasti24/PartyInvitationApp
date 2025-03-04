using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        public int Id { get; set; }
         
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        public DateTime Date { get; set; } // ✅ Ensure this is present!

        [Required]
        public string Location { get; set; }

        public int InviteCount { get; set; } = 0;

        public List<Invitation> Invitations { get; set; } = new();
    }
}
