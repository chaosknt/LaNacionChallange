using System;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class UpdatePhoneNumber : BasePhoneNumber
    {
        public Guid? PhoneNumberId { get; set; }
    }
}