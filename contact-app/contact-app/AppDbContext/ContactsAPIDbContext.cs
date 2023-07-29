using contact_app.Model;
using Microsoft.EntityFrameworkCore;

namespace contact_app.AppDbContext
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> Contacts
        {
            get;
            set;
        }
    }
}
