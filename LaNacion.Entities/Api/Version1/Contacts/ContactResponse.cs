using System;
using System.Collections.Generic;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class ContactResponse
    {
        public Guid ContactId { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ProfileImage { get; set; }

        public string BirthDate { get; set; }

        public IEnumerable<PhoneNumberResponse> PhoneNumbers { get; set; }
    }
}
