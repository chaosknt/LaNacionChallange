using System;
using System.Collections.Generic;

namespace LaNacion.Data.Service.Models
{
    public class Contact
    {
        public Guid ContactId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ProfileImage { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
    }
}
