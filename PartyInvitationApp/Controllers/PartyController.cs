using PartyInvitationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
//using PartyInvitationApp.Models;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;



namespace PartyInvitationApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Party
        public async Task<IActionResult> Index()
        {
            var parties = await _context.Parties
                .Include(p => p.Invitations) // ✅ Ensure invitations are loaded
                .Select(p => new Party
                {
                    Id = p.Id,
                    Description = p.Description,
                    Date = p.Date,
                    Location = p.Location,
                    Invitations = p.Invitations // ✅ Load invitations for correct count
                })
                .ToListAsync();

            return View(parties);
        }



        // GET: Party/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Party/Create
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

        // GET: Party/Edit/5
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

        // POST: Party/Edit/5
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

        // GET: Party/Manage/5
        public async Task<IActionResult> Manage(int id)
        {
            var party = await _context.Parties
                .Include(p => p.Invitations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

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
                // ✅ Pass the invitation ID when calling SendEmail
                SendEmail(invite.GuestEmail, invite.GuestName, party.Description, party.Location, party.Date, invite.Id);

                // Mark invitation as sent
                invite.Status = InvitationStatus.InviteSent;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Manage", new { id = partyId });
        }

        private void SendEmail(string guestEmail, string guestName, string partyName, string location, DateTime date, int invitationId)
        {
            var responseLink = $"http://localhost:5002/Invitation/Respond/{invitationId}";

            var fromAddress = new MailAddress("flamebondliu@gmail.com", "Party Manager App");
            var toAddress = new MailAddress(guestEmail, guestName);
            const string fromPassword = "vvdogzoqedfqswxt";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = $"You're Invited to {partyName}!",
                Body = $@"
            <h1>Hello {guestName},</h1>
            <p>You have been invited to <strong>{partyName}</strong> at <strong>{location}</strong> on <strong>{date:MM/dd/yyyy}</strong>.</p>
            <p>We would be thrilled to have you! Please let us know your availability by clicking the link below:</p>
            <p><a href='{responseLink}'>Respond to Invitation</a></p>
            <p>Sincerely,<br>The Party Manager App</p>",
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}