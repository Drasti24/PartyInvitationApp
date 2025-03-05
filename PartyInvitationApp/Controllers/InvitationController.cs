using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;
using System.Threading.Tasks;

namespace PartyInvitationApp.Controllers
{
    public class InvitationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvitationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invitation/Respond/{id}
        public async Task<IActionResult> Respond(int id)
        {
            var invitation = await _context.Invitations
                .Include(i => i.Party)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invitation == null)
            {
                return NotFound();
            }

            return View(invitation);
        }

        // POST: Invitation/Respond
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Respond(int id, bool isAttending)
        {
            var invitation = await _context.Invitations
                .Include(i => i.Party)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invitation == null)
            {
                return NotFound();
            }

            // Update the invitation status based on response
            invitation.Status = isAttending ? InvitationStatus.RespondedYes : InvitationStatus.RespondedNo;
            await _context.SaveChangesAsync();

            // Redirect to the Thank You page
            return RedirectToAction(nameof(ThankYou), new { guestName = invitation.GuestName, isAttending });
        }

        // GET: Invitation/ThankYou
        public IActionResult ThankYou(string guestName, bool isAttending)
        {
            ViewData["GuestName"] = guestName;
            ViewData["IsAttending"] = isAttending;
            return View();
        }
    }
}
