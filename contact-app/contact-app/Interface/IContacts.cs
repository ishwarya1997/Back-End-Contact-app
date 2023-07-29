using contact_app.Model;

namespace contact_app.Interface
{
    public interface IContacts
    {
        Task<Contact> GetContactById(Guid id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> CreateContact(Contact contact);
        Task<Contact> UpdateContact(Guid id, Contact contact);
        Task<bool> DeleteContact(Guid id);
    }
}
