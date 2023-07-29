using contact_app.AppDbContext;
using contact_app.Model;
using contact_app.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace contact_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private ContactsAPIDbContext dbContext;
        private readonly ContactService _contactService;
        public ContactsController(ContactsAPIDbContext dbContext, ContactService contactService)
        {
            this.dbContext = dbContext;
            _contactService = contactService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContacts();
            return Ok(contacts);
           
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, Contact updateContactRequest)
        {
            var contact = await _contactService.UpdateContact(id,updateContactRequest);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _contactService.DeleteContact(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(Contact addContactRequest)
        {
            try
            {
                var contact = new Contact()
                {
                    id = Guid.NewGuid(),
                    FirstName = addContactRequest.FirstName,
                    LastName = addContactRequest.LastName,
                    Email = addContactRequest.Email,
                    PhoneNumber = addContactRequest.PhoneNumber,
                    Address = addContactRequest.Address,
                    City = addContactRequest.City,
                    State = addContactRequest.State,
                    Country = addContactRequest.Country,
                    PostalCode = addContactRequest.PostalCode,
                };

               var createcontact=await _contactService.CreateContact(contact);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                // Log the exception here (you can use a logger or Console.WriteLine for now)
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while saving the entity changes.");
            }
        }

    }
}
