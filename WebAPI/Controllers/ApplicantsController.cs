using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [EnableCors("Policy")]
    
    public class ApplicantsController : ControllerBase
    {
        private readonly DataContext _context;

        public ApplicantsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Applicants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
            return await _context.Applicants.ToListAsync();
        }

        // GET: api/Applicants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicant(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }

            return applicant;
        }

        // PUT: api/Applicants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicant(int id, Applicant applicant)
        {
            if (id != applicant.ReferenceNo)
            {
                return BadRequest();
            }

            _context.Entry(applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Applicants
        [HttpPost]
        public async Task<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            await _context.SaveChangesAsync();

            //Notify
            ApplyNotify(applicant.Email);

            return CreatedAtAction("GetApplicant", new { id = applicant.ReferenceNo }, applicant);
        }

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Applicant>> DeleteApplicant(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();

            return applicant;
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicants.Any(e => e.ReferenceNo == id);
        }

        //Custom Functions

        public void ApplyNotify(string email)
        {
            MailMessage mail;

            //Preparing to send the mail
            string sender = "system.noreplyemail@gmail.com";
            string subject = "mySkol Admission";
            string message = "Good day ,we have receieved your application and it's been proceeed. ";
            string password = "Admin@01**";
            string smtp = "smtp.gmail.com";

             mail  = new MailMessage(sender.Trim(), email, subject.Trim(), message.Trim());
            SmtpClient confSmtp = new SmtpClient(smtp.Trim());
            confSmtp.Port = 587;
            
            confSmtp.Credentials = new System.Net.NetworkCredential(sender.Trim(), password.Trim());
            confSmtp.EnableSsl = true;
            confSmtp.Send(mail);



        }

    }
}
