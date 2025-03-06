//DRASTI PATEL
//PROBLEM ANALYSIS 2
//MARCH 09, 2025 

using PartyInvitationApp.Models;
using PartyInvitationApp.Services; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace PartyInvitationApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor: Initializes the database context for database operations
        public PartyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Party
        // Retrieves all parties along with their invitations and displays them in the index view
        public async Task<IActionResult> Index()
        {
            var parties = await _context.Parties
                .Include(p => p.Invitations)      // Load related invitations
                .Select(p => new Party
                {
                    Id = p.Id,
                    Description = p.Description,
                    Date = p.Date,
                    Location = p.Location,
                    Invitations = p.Invitations 
                })
                .ToListAsync();

            return View(parties);
        }

        // GET: Party/Create
        // Displays the form to create a new party
        public IActionResult Create()
        {
            return View();
        }

        // POST: Party/Create
        // Handles the submission of a new party creation form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Party party)
        {
            if (ModelState.IsValid)
            {
                _context.Parties.Add(party);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(party);
        }

        // GET: Party/Edit/{id}
        // Displays the edit form for a specific party
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Party/Edit/{id}
        // Handles form submission to edit a party
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Party party)
        {
            if (id != party.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(party);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Parties.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(party);
        }

        // GET: Party/Manage/{id}
        // Displays a page to manage a specific party, including viewing invitations
        public async Task<IActionResult> Manage(int id)
        {
            var party = await _context.Parties
                .Include(p => p.Invitations)       // Load invitations for this party
                .FirstOrDefaultAsync(p => p.Id == id);

            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Party/AddInvitation
        // Adds a new invitation to the party
        [HttpPost]
        public async Task<IActionResult> AddInvitation(int partyId, string guestName, string guestEmail)
        {
            var party = await _context.Parties.FindAsync(partyId);
            if (party == null)
            {
                return NotFound();
            }

            var invitation = new Invitation
            {
                PartyId = partyId,
                GuestName = guestName,
                GuestEmail = guestEmail,
                Status = InvitationStatus.InviteNotSent
            };

            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage", new { id = partyId });
        }

        // POST: Party/SendInvitations
        [HttpPost]
        public async Task<IActionResult> SendInvitations(int partyId)
        {
            var party = await _context.Parties
                .Include(p => p.Invitations)
                .FirstOrDefaultAsync(p => p.Id == partyId);

            if (party == null)
            {
                return NotFound();
            }

            foreach (var invite in party.Invitations.Where(i => i.Status == InvitationStatus.InviteNotSent))
            {
                // ✅ Call EmailService instead of defining email logic here
                EmailService.SendInvitation(invite.GuestEmail, invite.GuestName, party.Description, party.Location, party.Date, invite.Id);

                // Mark invitation as sent
                invite.Status = InvitationStatus.InviteSent;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", new { id = partyId });
        }

        [HttpPost]
        public async Task<IActionResult> SendInvitation(int partyId)
        {
            var party = await _context.Parties
                .Include(p => p.Invitations)
                .FirstOrDefaultAsync(p => p.Id == partyId);

            if (party == null)
            {
                return NotFound();
            }

            foreach (var invite in party.Invitations.Where(i => i.Status == InvitationStatus.InviteNotSent))
            {
                // ✅ Call EmailService instead of having email logic here
                EmailService.SendInvitation(invite.GuestEmail, invite.GuestName, party.Description, party.Location, party.Date, invite.Id);

                // Mark invitation as sent
                invite.Status = InvitationStatus.InviteSent;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", new { id = partyId });
        }


        // POST: Party/Delete/{id}
        // Deletes a party along with its invitations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var party = await _context.Parties
                .Include(p => p.Invitations)          // Load related invitations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (party == null)
            {
                return NotFound();
            }

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}