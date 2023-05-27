using LaNacion.Entities.Enums;
using System;

namespace LaNacion.Data.Service.Models
{
    public class PhoneNumber
    {
        public Guid PhoneNumberId { get; set; }

        public Guid ContactId { get; set; }

        public NumberTypes Type { get; set; }

        public string Number { get; set; }
    }
}
