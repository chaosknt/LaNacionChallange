using LaNacion.Common;
using LaNacion.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LaNacion.Entities.Api.Version1.Contacts
{
    [ModelBinder(BinderType = typeof(MetadataValueModelBinder))]
    public class BasePhoneNumber
    {
        [Required]
        [EnumDataType(typeof(NumberTypes))]
        public int Type { get; set; }

        [StringLength(20)]
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter numbers only")]

        public string Number { get; set; }
    }
}
