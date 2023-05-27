using LaNacion.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Stores
{
    public interface IPhoneNumberStore
    {
        Task CreateAsync(IEnumerable<PhoneNumberDbEntity> phones);

        Task UpdateAsync(IEnumerable<PhoneNumberDbEntity> phones);

        Task<IQueryable<PhoneNumberDbEntity>> GetAllByAsync(Expression<Func<PhoneNumberDbEntity, bool>> filter = null);

        Task<PhoneNumberDbEntity> GetByAsync(Expression<Func<PhoneNumberDbEntity, bool>> filter);

        Task DeleteAsync(IEnumerable<Guid> phonesIds);
    }
}
