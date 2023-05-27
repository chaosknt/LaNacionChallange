using LaNacion.Data.Models;
using LaNacion.Data.Service.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Stores
{
    public interface IContactStore
    {
        Task<ContactDbEntity> CreateAsync(Contact model);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Contact model);

        Task<IQueryable<ContactDbEntity>> GetAllByAsync(Expression<Func<ContactDbEntity, bool>> filter = null);
        
        Task<ContactDbEntity> GetByAsync(Expression<Func<ContactDbEntity, bool>> filter);
    }
}