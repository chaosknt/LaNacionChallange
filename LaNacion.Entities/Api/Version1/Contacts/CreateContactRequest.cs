using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class CreateContactRequest : BaseContactRequest
    {
        [FromForm(Name = "PhoneNumbers")]
        public List<BasePhoneNumber> PhoneNumbers { get; set; }

    }
}
