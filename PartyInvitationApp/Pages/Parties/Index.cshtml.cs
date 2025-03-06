using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;

namespace PartyInvitationApp.Pages.Parties
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Holds the list of all parties retrieved from the database
        public List<Party> Parties { get; set; } = new();

        // Constructor: Injects the database context
        public IndexModel(ApplicationDbContext context) => _context = context;

        // Handles GET requests for the page
        public async Task OnGetAsync()
        {
            // Retrieves all parties along with their invitations from the database
            Parties = await _context.Parties.Include(p => p.Invitations).ToListAsync();
        }
    }
}
