using ITT.CRM.Domain;

namespace ITT.CRM.Core
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>?> GetAllContactsAsync();
        Task<Contact?> GetContactByIdAsync(int id);
        Task<Contact?> AddContactAsync(Contact contact);
        Task<Contact?> UpdateContactAsync(Contact contact);
        Task<Contact?> DeleteContactAsync(int contactId);
        Task<IEnumerable<Contact>?> SearchContactsAsync(string searchTerm);

    }
}
