using LaNacion.Common.Enum;
using LaNacion.Data.Service.Exceptions;
using LaNacion.Data.Service.Managers;
using LaNacion.Entities.Api.Version1.Contacts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaNacion.Data.Service.Validators
{
    public static class ContactsValidation
    {
        public static void ValidateDuplicates(IEnumerable<BasePhoneNumber> phones)
        {
            if (phones.GroupBy(phone => phone.Number).Any(group => group.Count() > 1))
            {
                throw new BussinessException(ContactManager.DuplicateError, Common.Enum.KnownErrorCodesEnum.InvalidPayload);
            }
        }

        public static void ValidateImage(IFormFile image, int maxAllowedSizeInMb, IEnumerable<string> extensions )
        {
            if (image == null)
            {
                return;
            }

            if (image.Length > maxAllowedSizeInMb)
            {
                throw new BussinessException($"La imagen es demasiado pesada", KnownErrorCodesEnum.InvalidPayload);
            }

            if (!extensions.Contains(image.ContentType))
            {
                throw new BussinessException($"La extension de la imagen es incorrecta", KnownErrorCodesEnum.InvalidPayload);
            }
        }
    }
}
