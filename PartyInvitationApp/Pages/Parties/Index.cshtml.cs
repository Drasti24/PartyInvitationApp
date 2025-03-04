using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;

namespace PartyInvitationApp.Pages.Parties
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Party> Parties { get; set; } = new();

        public IndexModel(ApplicationDbContext context) => _context = context;

        public async Task OnGetAsync() => Parties = await _context.Parties.Include(p => p.Invitations).ToListAsync();
    }
}
