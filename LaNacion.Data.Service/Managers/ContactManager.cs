using LaNacion.Common;
using LaNacion.Common.Filters;
using LaNacion.Common.Helpers.File;
using LaNacion.Common.Reflexion;
using LaNacion.Common.Search;
using LaNacion.Common.Search.Sort;
using LaNacion.Data.Models;
using LaNacion.Data.Service.Exceptions;
using LaNacion.Data.Service.Mappers;
using LaNacion.Data.Service.Models;
using LaNacion.Data.Service.Stores;
using LaNacion.Data.Service.Validators;
using LaNacion.Entities.Api.Version1.Contacts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaNacion.Data.Service.Managers
{
    public class ContactManager : IContactManager
    {
        private const string BaseFilePath = "Contacts";
        private readonly AppSettingsConfigurationService apiConfigurationService;
        private readonly IContactStore contactStore;
        private readonly IPhoneNumberStore phoneNumberStore;

        private const string EmailError = "The Email has been already registered";
        private const string PhoneNumberError = "One of the entered telephone numbers is already registered";
        private const string PhoneNumberIsRequiredError = "At least, one phone number is required";
        public static string DuplicateError = "There is duplicate numbers";

        public ContactManager(IContactStore contactStore, IPhoneNumberStore phoneNumberStore, AppSettingsConfigurationService configuration)
        {
            this.contactStore = contactStore;
            this.phoneNumberStore = phoneNumberStore;
            this.apiConfigurationService = configuration;
        }

        public async Task<Guid> CreateAsync(CreateContactRequest request)
        {
            if (!request.PhoneNumbers.Any())
            {
                throw new BussinessException(PhoneNumberIsRequiredError, Common.Enum.KnownErrorCodesEnum.PhoneNumberError);
            }

            ContactsValidation.ValidateDuplicates(request.PhoneNumbers);

            if (!await this.PhoneNumberAvailable(request.PhoneNumbers.Select(x => x.Number).ToList()))
            {
                throw new BussinessException(PhoneNumberError, Common.Enum.KnownErrorCodesEnum.InvalidPayload);
            }

            if (!await this.EmailAvailable(request.Email))
            {
                throw new BussinessException(EmailError, Common.Enum.KnownErrorCodesEnum.InvalidPayload);
            }

            var result = await this.contactStore.CreateAsync(request.ToModel(await this.GetImagePath(request.ProfileImage)));

            await this.phoneNumberStore.CreateAsync(request.PhoneNumbers.Select(contact => contact.ToEntity(result.ContactId)).ToList());

            return result.ContactId;
        }

        public async Task DeleteAsync(Guid id)
        {
            await this.contactStore.DeleteAsync((await this.GetContactById(id)).ContactId);
        }

        public async Task<Common.Search.SearchResult<Contact>> FindByLocationAsync(SearchFilter filter)
        {
            var contacts = await this.FindContacts(ExpressionHelper.CreateFilterContainsExpression<ContactDbEntity>(filter.ParamName, filter.ParamVal), filter.Sorter);

            var totalItems = contacts.Count();
            var partialData = contacts.Skip(filter.Start).Take(filter.Length).Select(x => x.ToModel());

            return new Common.Search.SearchResult<Contact>(partialData, contacts.Count());
        }

        public async Task<Contact> GetByIdAsync(Guid id)
            => (await this.GetContactById(id)).ToModel();

        public async Task<Contact> SearchAsync(SearchFilter filter)
        {
            var contacts = ReflectionHelper.DoesPropertyExist(typeof(ContactDbEntity), filter.ParamName) 
                           ? await this.FindContacts(ExpressionHelper.CreateFilterEqualExpression<ContactDbEntity>(filter.ParamName,filter.ParamVal), filter.Sorter)
                           : await this.FindContacts(await this.FindContactsByPhoneFilter(filter.ParamName, filter.ParamVal));

            var totalItems = contacts.Count();
            var partialData = contacts.Skip(filter.Start).Take(filter.Length).Select(x => x.ToModel());

            return contacts.FirstOrDefault()?.ToModel();
        }

        public async Task UpdateAsync(UpdateContactRequest request)
        {
            var contact = await this.GetContactById(request.Id);

            if (!request.PhoneNumbers.Any())
            {
                throw new BussinessException(PhoneNumberIsRequiredError, Common.Enum.KnownErrorCodesEnum.PhoneNumberError);
            }

            ContactsValidation.ValidateDuplicates(request.PhoneNumbers);
            if (request.Email != contact.Email && !await this.EmailAvailable(request.Email))
            {
                throw new BussinessException(EmailError, Common.Enum.KnownErrorCodesEnum.InvalidPayload);
            }

            if (!await this.PhoneNumberAvailable(request.PhoneNumbers.Select(x => x.Number).Except(contact.PhoneNumbers.Select(x => x.Number))))
            {
                throw new BussinessException(PhoneNumberError, Common.Enum.KnownErrorCodesEnum.InvalidPayload);
            }

            await this.contactStore.UpdateAsync(request.ToModel(await this.UpdateImage(contact.ProfileImage, request.ProfileImage, request.UpdateProfileImage), contact.ContactId));
            await this.SyncPhoneNumbers(contact.PhoneNumbers.ToList(), request.PhoneNumbers);
        }

        private async Task<string> GetImagePath(IFormFile image, bool update = true)
        {
            if (!update || image == null)
            {
                return null;
            }

            ContactsValidation.ValidateImage(image, this.apiConfigurationService.ImageMaxAllowedSizeInMB, this.apiConfigurationService.AvailableImageExtensions);
            return await FileManager.SaveFileAsync(
                                    FileManager.PathCombine(this.apiConfigurationService.ImagesOutputPath, BaseFilePath),
                                    image.FileName,
                                    image.ContentType,
                                    FileManager.GetFileBytes(image));
        }

        private Task<string> UpdateImage(string currentImage, IFormFile image, bool update = true)
        {
            if(update && currentImage != null)
            {
                FileManager.RemoveFileIfExists(currentImage);
            }

            return this.GetImagePath(image, update);
        }

        private async Task<ContactDbEntity> GetContactById(Guid id)
        {
            var contact = await this.contactStore.GetByAsync(contact => contact.ContactId == id);
            if (contact == null)
            {
                throw new BussinessException("Contact not found", Common.Enum.KnownErrorCodesEnum.InvalidPayload);
            }

            contact.PhoneNumbers = (await this.phoneNumberStore.GetAllByAsync(p => p.ContactId == contact.ContactId)).ToList();

            return contact;
        }

        private async Task SyncPhoneNumbers(IEnumerable<PhoneNumberDbEntity> currentPhoneNumbers, IEnumerable<UpdatePhoneNumber> incomingPhoneNumbers)
        {
            var numbersToDelete = currentPhoneNumbers.Where(phone => !incomingPhoneNumbers.Any(incoming => incoming.Number == phone.Number))
                                                     .Select(phone => phone.PhoneNumberId);

            var numbersTCreateOrUpdate = new List<UpdatePhoneNumber>();
            foreach (var phoneNumber in incomingPhoneNumbers)
            {
                var currentNumber = currentPhoneNumbers.FirstOrDefault(phone => phone.PhoneNumberId == phoneNumber.PhoneNumberId);
                if(currentNumber == null)
                {
                    numbersTCreateOrUpdate.Add(phoneNumber);
                    continue;
                }

                currentNumber.Type = (int)phoneNumber.Type;
                currentNumber.Number = phoneNumber.Number;
                numbersTCreateOrUpdate.Add(phoneNumber);
            }

            await this.phoneNumberStore.CreateAsync(numbersTCreateOrUpdate.Where(phone => !phone.PhoneNumberId.HasValue && currentPhoneNumbers.FirstOrDefault(x => x.Number == phone.Number) == null).Select(contact => contact.ToEntity(currentPhoneNumbers.First().ContactId)));
            await this.phoneNumberStore.UpdateAsync(numbersTCreateOrUpdate.Where(phone => phone.PhoneNumberId.HasValue).Select(phone => phone.ToEntity(currentPhoneNumbers.First().ContactId, phone.PhoneNumberId)));
            await this.phoneNumberStore.DeleteAsync(numbersToDelete);
        }

        private async Task<bool> EmailAvailable(string email)
           => !(await this.contactStore.GetAllByAsync(contact => contact.Email.ToLower() == email.ToLower())).Any();

        private async Task<bool> PhoneNumberAvailable(IEnumerable<string> phoneNumbers)
            => !(await this.phoneNumberStore.GetAllByAsync(phone => phoneNumbers.Contains(phone.Number))).Any();

        private async Task<IEnumerable<ContactDbEntity>> FindContacts(Expression<Func<ContactDbEntity, bool>> filter, SortFilter Sorter = null)
        {
            var contacts = await this.contactStore.GetAllByAsync(filter);
            if (!contacts.Any())
            {
                return Enumerable.Empty<ContactDbEntity>();
            }

            if(Sorter != null)
            {
                contacts = Sorter.SortOrder == SortOrder.Ascending ? contacts.OrderBy(x => x.Name) : contacts.OrderByDescending(x => x.Name);
            }

            var phones = await this.phoneNumberStore.GetAllByAsync(x => contacts.Select(x => x.ContactId).Contains(x.ContactId));
            return this.RelateContacts(contacts.ToList(), phones);
        }

        private async Task<Expression<Func<ContactDbEntity, bool>>> FindContactsByPhoneFilter(string filterName, string filterValue)
        {
            var phone = await this.phoneNumberStore.GetByAsync(ExpressionHelper.CreateFilterEqualExpression<PhoneNumberDbEntity>(filterName, filterValue));
            if(phone == null)
            {
                return null;
            }

            return x => x.ContactId == phone.ContactId;
        }

        private IEnumerable<ContactDbEntity> RelateContacts(IEnumerable<ContactDbEntity> contacts, IEnumerable<PhoneNumberDbEntity> phones)
        {
            foreach (var contact in contacts)
            {
                contact.PhoneNumbers = phones.Where(x => x.ContactId == contact.ContactId).ToList();
            }

            return contacts;
        }
    }
}