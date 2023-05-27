using LaNacion.Data.Models;
using LaNacion.Data.Service.Models;
using LaNacion.Entities.Api.Version1.Contacts;
using System;
using System.Linq;

namespace LaNacion.Data.Service.Mappers
{
    public static class ContactMappers
    {
        public static Contact ToModel(this BaseContactRequest request, string profileImage = null, Guid? ContactId = null)
            => request == null ? default : new Contact()
            {
                ContactId = ContactId ?? Guid.Empty,
                AddressLine = request.AddressLine,
                BirthDate = request.BirthDate,
                City = request.City,
                Company = request.Company,
                Name = request.Name,
                ProfileImage = profileImage,
                State = request.State,
                Email = request.Email,
            };

        public static Contact ToModel(this ContactDbEntity entity)
            => entity == null ? default : new Contact()
            {
                ContactId = entity.ContactId,
                AddressLine = entity.AddressLine,
                BirthDate = entity.BirthDate,
                City = entity.City,
                Company = entity.Company,
                Name = entity.Name,
                ProfileImage = entity.ProfileImage,
                State = entity.State,
                Email = entity.Email,
                PhoneNumbers = entity.PhoneNumbers.Select(p => p.ToModel())
            };
    }
}
