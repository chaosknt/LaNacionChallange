using System;
using System.Collections.Generic;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class UpdateContactRequest : BaseContactRequest
    {
        public Guid Id { get; set; }

        public bool UpdateProfileImage { get; set; }

        public IEnumerable<UpdatePhoneNumber> PhoneNumbers { get; set; }
    }
}
