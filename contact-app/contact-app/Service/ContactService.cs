using contact_app.AppDbContext;
using contact_app.Interface;
using contact_app.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace contact_app.Service
{
    public class ContactService:IContacts
    {
        private readonly ContactsAPIDbContext _dbContext;

        public ContactService(ContactsAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private int _nextId = 1; // Initialize the ID counter.
        public async Task<Contact> CreateContact(Contact contact)
        {
            try
            {
                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();
                return contact;
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error occurred while saving contact: " + ex.Message);
                throw;
            }
        }

        public async Task<Contact> GetContactById(Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            return contact;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var allContacts = await _dbContext.Contacts.OrderByDescending(c => c.id).ToListAsync();
            return allContacts;
        }


        public async Task<Contact> UpdateContact(Guid id, Contact updateContactRequest)
        {
            var updatecontact = await _dbContext.Contacts.FindAsync(id);
            if (updatecontact != null)
            {
                updatecontact.FirstName = updateContactRequest.FirstName;
                updatecontact.LastName = updateContactRequest.LastName;
                updatecontact.Email = updateContactRequest.Email;
                updatecontact.PhoneNumber = updateContactRequest.PhoneNumber;
                updatecontact.Address = updateContactRequest.Address;
                updatecontact.City = updateContactRequest.City;
                updatecontact.State = updateContactRequest.State;
                updatecontact.Country = updateContactRequest.Country;
                updatecontact.PostalCode = updateContactRequest.PostalCode;
                await _dbContext.SaveChangesAsync();
                return updatecontact;
            }
            return updatecontact;
        }

        public async Task<bool> DeleteContact(Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                _dbContext.Remove(contact);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
