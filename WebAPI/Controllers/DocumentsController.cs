using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentsController(DataContext context)
        {
            _context = context;
        }

		

		// GET: api/Documents
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

				if (document == null)
				{
					return NotFound();
				}

            return document;
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.FileId)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(string fName, int AppId,IFormFile Attachment)
        {

			using (var memoryStream = new MemoryStream())
			{
				await Attachment.CopyToAsync(memoryStream);

				// Upload the file if less than 2 MB
				if (memoryStream.Length < 2097152)
				{
					var File = new Document()
					{
						FileName = fName,
						ApplicantId = AppId,						
						File = memoryStream.ToArray()
					};

					_context.Documents.Add(File);
					await _context.SaveChangesAsync();
				}
				else
				{
					ModelState.AddModelError("File", "The file is too large.");
				}
			}
			return Ok();	
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return document;
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.ApplicantId == id);
        }
    }
}
