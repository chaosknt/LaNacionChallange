using LaNacion.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Stores
{
    public class PhoneNumberStore : BaseStore<PhoneNumberDbEntity>, IPhoneNumberStore
    {
        public PhoneNumberStore(LaNacionContext context)
              : base(context, context => context.PhoneNumbers)
        {
        }

        public async Task CreateAsync(IEnumerable<PhoneNumberDbEntity> phones)
        {
           await this.SaveEntities(phones);
        }

        public async Task DeleteAsync(IEnumerable<Guid> phonesIds)
        {
            var entities = await this.FilterEntities(p => phonesIds.Contains(p.PhoneNumberId));
            await this.RemoveEntities(entities);
        }

        public async Task<IQueryable<PhoneNumberDbEntity>> GetAllByAsync(Expression<Func<PhoneNumberDbEntity, bool>> filter = null)
            => await this.FilterEntities(filter);

        public async Task<PhoneNumberDbEntity> GetByAsync(Expression<Func<PhoneNumberDbEntity, bool>> filter)
            => (await this.GetAllByAsync(filter)).FirstOrDefault();

        public async Task UpdateAsync(IEnumerable<PhoneNumberDbEntity> phones)
        {
            var entities = await this.FilterEntities(phone => phones.Select(x => x.PhoneNumberId).Contains(phone.PhoneNumberId));
            foreach (var phoneNumber in phones)
            {
                var current = entities.First(p => p.PhoneNumberId == phoneNumber.PhoneNumberId);
                current.Number = phoneNumber.Number;
                current.Type = phoneNumber.Type;
            }

            await this.SaveChangesAsync();
        }
    }
}
