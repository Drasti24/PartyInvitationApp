//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025 

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

        // Constructor: Initializes the database context for database operations
        public InvitationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invitation/Respond/{id}
        // This method retrieves an invitation by ID and displays the response form
        public async Task<IActionResult> Respond(int id)
        {
            // Fetch the invitation along with the associated party from the database
            var invitation = await _context.Invitations
                .Include(i => i.Party)
                .FirstOrDefaultAsync(i => i.Id == id);

            // If no invitation is found, return a 404 Not Found response
            if (invitation == null)
            {
                return NotFound();
            }

            return View(invitation);
        }

        // POST: Invitation/Respond
        // This method handles the form submission when a guest responds to an invitation
        [HttpPost]
        [ValidateAntiForgeryToken]     // Ensures request security against CSRF attacks
        public async Task<IActionResult> Respond(int id, bool isAttending)
        {
            // Fetch the invitation along with the associated party from the database
            var invitation = await _context.Invitations
                .Include(i => i.Party)
                .FirstOrDefaultAsync(i => i.Id == id);

            // If no invitation is found, return a 404 Not Found response
            if (invitation == null)
            {
                return NotFound();
            }

            // Update the invitation status based on guest's response
            invitation.Status = isAttending ? InvitationStatus.RespondedYes : InvitationStatus.RespondedNo;
            await _context.SaveChangesAsync();

            // Redirect to the Thank You page with guest name and response status
            return RedirectToAction(nameof(ThankYou), new { guestName = invitation.GuestName, isAttending });
        }

        // GET: Invitation/ThankYou
        // This method displays the thank-you message after a guest responds
        public IActionResult ThankYou(string guestName, bool isAttending)
        {
            // Pass guest name and response status to the view
            ViewData["GuestName"] = guestName;
            ViewData["IsAttending"] = isAttending;
            return View();
        }
    }
}
