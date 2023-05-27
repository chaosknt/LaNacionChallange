using LaNacion.Data.Models;
using LaNacion.Data.Service.Models;
using LaNacion.Entities.Api.Version1.Contacts;
using System;

namespace LaNacion.Data.Service.Mappers
{
    public static class PhoneNumberMappers
    {
        public static PhoneNumberDbEntity ToEntity(this BasePhoneNumber model, Guid contactId, Guid? phoneNumberId = null)
            => model == null ? default : new PhoneNumberDbEntity()
            {
                PhoneNumberId = phoneNumberId ?? Guid.Empty,
                ContactId = contactId,
                Number = model.Number,
                Type = model.Type
            };

        public static PhoneNumber ToModel(this PhoneNumberDbEntity entity)
            => entity == null ? default : new PhoneNumber()
            {
                PhoneNumberId = entity.PhoneNumberId,
                ContactId = entity.ContactId,
                Number = entity.Number,
                Type = (Entities.Enums.NumberTypes)entity.Type
            };
    }
}
