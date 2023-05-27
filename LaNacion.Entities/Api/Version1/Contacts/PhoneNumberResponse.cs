using LaNacion.Entities.Enums;
using System;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class PhoneNumberResponse
    {
        public Guid PhoneNumberId { get; set; }

        public NumberTypes Type { get; set; }

        public string Number { get; set; }
    }
}
