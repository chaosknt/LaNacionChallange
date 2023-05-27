using LaNacion.Data.Service.Models;
using LaNacion.Entities.Api.Version1.Contacts;
using System.Linq;

namespace LaNacion.Api.Mappers
{
    public static class ContactMappers
    {
        public static ContactResponse ToResponseModel(this Contact contact)
            => contact == null ? default : new ContactResponse()
            {
                AddressLine = contact.AddressLine,
                BirthDate = contact.BirthDate.ToString("yyyy-MM-dd"),
                City = contact.City,
                Company = contact.Company,
                ContactId = contact.ContactId,
                Email = contact.Email,
                Name = contact.Name,
                PhoneNumbers = contact.PhoneNumbers.Select(p => p.ToResponseModel()),
                ProfileImage = contact.ProfileImage,
                State = contact.State
            };

        public static PhoneNumberResponse ToResponseModel(this PhoneNumber phoneNumber)
            => phoneNumber == null ? default : new PhoneNumberResponse()
            {
                PhoneNumberId = phoneNumber.PhoneNumberId,
                Number = phoneNumber.Number,
                Type = phoneNumber.Type
            };
    }
}
