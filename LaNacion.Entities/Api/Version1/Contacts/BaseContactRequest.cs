using LaNacion.Common.Helpers.Attribute;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    public class BaseContactRequest
    {
        [Required]
        [StringLength(48, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(48, MinimumLength = 3)]
        public string Company { get; set; }

        [Required]
        [StringLength(40)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [RegularExpression(@"^[A-Za-z\s]+\d+.*$", ErrorMessage = "Address must have street and number")]
        public string AddressLine { get; set; }

        [Required]
        [StringLength(48)]
        public string City { get; set; }

        [Required]
        [StringLength(48)]
        public string State { get; set; }

        public IFormFile ProfileImage { get; set; }


        [Required]
        [ValidateBirthDate]
        public DateTime BirthDate { get; set; }
    }
}
