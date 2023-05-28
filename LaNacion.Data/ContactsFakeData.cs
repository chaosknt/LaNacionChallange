using LaNacion.Common;
using LaNacion.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaNacion.Data
{
    public class ContactsFakeData
    {
        public static string contact1Email = "Dora.smith@example.com";
        public static string contact1PhoneNumber = "555-1111";

        public IEnumerable<ContactDbEntity> Contacts { get; set; }

        public IEnumerable<PhoneNumberDbEntity> Phones { get; set; }

        public ContactsFakeData()
        {

            var contact1 = new ContactDbEntity
            {
                ContactId = Guid.NewGuid(),
                Name = "Dora Smith",
                Company = "ABC Company",
                Email = contact1Email,
                AddressLine = "123 Main St",
                City = "New York",
                State = "New York",
                ProfileImage = null,
                BirthDate = new DateTime(2010, 1, 1)
            };

            var contact2 = new ContactDbEntity
            {
                ContactId = Guid.NewGuid(),
                Name = "Jane Doe",
                Company = "XYZ Corporation",
                Email = "jane.doe@example.com",
                AddressLine = "456 Elm St",
                City = "Los Angeles",
                State = "California",
                ProfileImage = null,
                BirthDate = new DateTime(1995, 3, 15)
            };

            var contact3 = new ContactDbEntity
            {
                ContactId = Guid.NewGuid(),
                Name = "Aby Doe",
                Company = "ABC Company",
                Email = "Aby.smith@example.com",
                AddressLine = "123 Main St",
                City = "New York",
                State = "New York",
                ProfileImage = null,
                BirthDate = new DateTime(1990, 1, 1)
            };

            this.Phones = new List<PhoneNumberDbEntity>
                {
                    new PhoneNumberDbEntity
                    {
                        PhoneNumberId = new Guid(),
                        ContactId = contact1.ContactId,
                        Type = 1,
                        Number = contact1PhoneNumber
                    },
                    new PhoneNumberDbEntity
                    {
                        PhoneNumberId = new Guid(),
                        ContactId = contact1.ContactId,
                        Type = 2,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    },
                    new PhoneNumberDbEntity
                    {
                        PhoneNumberId = new Guid(),
                        ContactId = contact2.ContactId,
                        Type = 1,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    },
                    new PhoneNumberDbEntity
                    {
                        PhoneNumberId = new Guid(),
                        ContactId = contact2.ContactId,
                        Type = 2,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    },
                     new PhoneNumberDbEntity
                    {
                        PhoneNumberId = new Guid(),
                        ContactId = contact3.ContactId,
                        Type = 1,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    },
                    new PhoneNumberDbEntity
                    {
                        PhoneNumberId = new Guid(),
                        ContactId = contact3.ContactId,
                        Type = 2,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    }
                };

            this.Contacts = new List<ContactDbEntity>()
            {
                contact1,
                contact2,
                contact3
            };
        }
    }
}
