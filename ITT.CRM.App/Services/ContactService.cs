using ITT.CRM.Core;
using ITT.CRM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ITT.CRM.App
{
    public class ContactService : IContactService
    {
        private readonly DataContext _dbContext;
        private readonly ILogger<ContactService> _logger;

        public ContactService(DataContext dbContext, ILogger<ContactService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>?> GetAllContactsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all contacts.");
                return await _dbContext.Contacts.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all contacts.");
                return Enumerable.Empty<Contact>();
            }
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching contact with ID: {id}");
                return await _dbContext.Contacts.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching contact with ID: {id}");
                return null;
            }
        }

        public async Task<Contact?> AddContactAsync(Contact contact)
        {
            try
            {
                _logger.LogInformation($"Adding new contact: {contact.Nom} {contact.Prenom}");
                await _dbContext.Contacts.AddAsync(contact);
                await _dbContext.SaveChangesAsync();
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding contact: {contact.Nom} {contact.Prenom}");
                return null;
            }
        }

        public async Task<Contact?> UpdateContactAsync(Contact updatedContact)
        {
            try
            {
                var contact = await _dbContext.Contacts.FindAsync(updatedContact.Id).ConfigureAwait(false);
                if (contact == null)
                {
                    _logger.LogWarning($"Contact with ID: {updatedContact.Id} not found.");
                    return null;
                }

                _logger.LogInformation($"Updating contact with ID: {updatedContact.Id}");

                contact.Civilite = updatedContact.Civilite;
                contact.Nom = updatedContact.Nom;
                contact.Prenom = updatedContact.Prenom;
                contact.Entreprise = updatedContact.Entreprise;
                contact.Fonction = updatedContact.Fonction;
                contact.Telephone = updatedContact.Telephone;
                contact.Email = updatedContact.Email;
                contact.Adresse = updatedContact.Adresse;

                _dbContext.Contacts.Update(contact);
                await _dbContext.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating contact with ID: {updatedContact.Id}");
                return null;
            }
        }

        public async Task<Contact?> DeleteContactAsync(int contactId)
        {
            try
            {
                var contact = await _dbContext.Contacts.FirstOrDefaultAsync(g => g.Id == contactId);
                if (contact != null)
                {
                    _logger.LogInformation($"Deleting contact with ID: {contactId}");
                    _dbContext.Contacts.Remove(contact);
                    await _dbContext.SaveChangesAsync();
                    return contact;
                }

                _logger.LogWarning($"Contact with ID: {contactId} not found.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting contact with ID: {contactId}");
                return null;
            }
        }

        public async Task<IEnumerable<Contact>?> SearchContactsAsync(string searchTerm)
        {
            try
            {
                _logger.LogInformation($"Searching contacts with term: {searchTerm}");

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetAllContactsAsync();
                }

                return await _dbContext.Contacts
                    .Where(c => c.Nom.Contains(searchTerm) || c.Prenom.Contains(searchTerm) || c.Email.Contains(searchTerm))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while searching contacts with term: {searchTerm}");
                return Enumerable.Empty<Contact>();
            }
        }
    }
}
