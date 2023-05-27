using LaNacion.Common.Search;
using LaNacion.Data.Service.Models;
using LaNacion.Entities.Api.Version1.Contacts;
using System;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Managers
{
    public interface IContactManager
    {
        Task<Guid> CreateAsync(CreateContactRequest request);

        Task<Contact> GetByIdAsync(Guid id);

        Task UpdateAsync(UpdateContactRequest request);

        Task DeleteAsync(Guid id);

        Task<Contact> SearchAsync(SearchFilter filter);

        Task<Common.Search.SearchResult<Contact>> FindByLocationAsync(SearchFilter filter);
    }
}