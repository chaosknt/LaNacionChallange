using System;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class CreateContactResponse : BasicResponse
    {
        public Guid ContactId { get; set; }
    }
}
