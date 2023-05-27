using LaNacion.Data.Models;
using LaNacion.Data.Service.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Stores
{
    public class ContactStore : BaseStore<ContactDbEntity>, IContactStore
    {
        private readonly IPhoneNumberStore phoneNumberStore;

        public ContactStore(LaNacionContext context, IPhoneNumberStore phoneNumberStore)
               : base(context, context => context.Contacts)
        {
            this.phoneNumberStore = phoneNumberStore;
        }

        public async Task<ContactDbEntity> CreateAsync(Contact model)
            =>  await this.SaveEntity(new ContactDbEntity()
                {
                    AddressLine = model.AddressLine,
                    BirthDate = model.BirthDate,
                    City = model.City,
                    Company = model.Company,
                    Name = model.Name,
                    ProfileImage = model.ProfileImage,
                    State = model.State,
                    Email = model.Email
                });                        

        public async Task DeleteAsync(Guid id) 
            => await this.RemoveEntity(await this.GetEntity(x => x.ContactId == id));

        public async Task<IQueryable<ContactDbEntity>> GetAllByAsync(Expression<Func<ContactDbEntity, bool>> filter = null)
            => (await this.FilterEntities(filter));

        public async Task<ContactDbEntity> GetByAsync(Expression<Func<ContactDbEntity, bool>> filter)
            => (await this.GetAllByAsync(filter)).FirstOrDefault();

        public async Task UpdateAsync(Contact model)
        {
            var entity = await this.GetEntity(x => x.ContactId == model.ContactId);
            if(entity == null)
            {
                return;
            }

            entity.AddressLine = model.AddressLine;
            entity.BirthDate = model.BirthDate;
            entity.City = model.City;
            entity.Company = model.Company;
            entity.Name = model.Name;
            entity.ProfileImage = model.ProfileImage;
            entity.State = model.State;
            entity.State = model.State;

            await this.SaveChangesAsync();
        }
    }
}