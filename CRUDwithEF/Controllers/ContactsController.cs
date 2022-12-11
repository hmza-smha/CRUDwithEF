using CRUDwithEF.Data;
using CRUDwithEF.DTOs.Contacts;
using CRUDwithEF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDwithEF.Controllers
{
    [Authorize(Roles = UserRoles.User+","+UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // if you use Swagger you need to specify [HttpGet]
        [HttpGet]
        //public IEnumerable<Contact> GetContacts()
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact != null)
            {
                return Ok(contact);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactDTO dto)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact(
            [FromRoute] Guid id, 
            [FromBody] ContactDTO dto)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if(contact != null)
            {
                contact.Name = dto.Name;
                contact.Email = dto.Email;
                contact.Phone = dto.Phone;
                contact.Address = dto.Address;

                await _context.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
