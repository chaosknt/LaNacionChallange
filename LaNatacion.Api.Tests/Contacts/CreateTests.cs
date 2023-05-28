using LaNacion.Api.Controllers.Api.Version1;
using LaNacion.Common;
using LaNacion.Data;
using LaNacion.Data.Service.Managers;
using LaNacion.Entities.Api.Version1.Contacts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LaNatacion.Api.Tests.Contacts
{
    public class CreateTests : BasePortalControllerTestClass<ContactController>
    {
        public CreateTests(TestServiceWebApplicationFactory factory) : base(factory)
        {
            factory.Init(TestEntities.Contact);
        }

        [Fact]
        public async Task ValidRequest_Return201()
        {
            // arrange

            var model = new CreateContactRequest()
            {
                Name = "Joe Washington",
                Company = "ABC Company",
                Email = "Joe.Washington@example.com",
                AddressLine = "123 Emet St",
                City = "New York",
                State = "New York",
                ProfileImage = null,
                BirthDate = new DateTime(2001, 1, 1),
                PhoneNumbers = new List<BasePhoneNumber>()
                {
                    new BasePhoneNumber
                    {
                        Type = 2,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    }
                }
            };

            var expectedStatus = StatusCodes.Status201Created;

            // act

            var res = (await this.Controller.Create(model)).Result;
            var response = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).Value;
            var actualStatus = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).StatusCode;
            var contactCreated = ((LaNacion.Entities.Api.Version1.Contacts.SearchResult<LaNacion.Entities.Api.Version1.Contacts.ContactResponse>)((Microsoft.AspNetCore.Mvc.ObjectResult)(await this.Controller.Get(((CreateContactResponse)response).ContactId, null)).Result).Value).Result;
            
            // assert

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal(model.Email, contactCreated.Email);
        }

        [Fact]
        public async Task DuplicateEmail_ReturnBadRequest()
        {
            // arrange

            var model = new CreateContactRequest()
            {
                Name = "Joe Washington",
                Company = "ABC Company",
                Email = ContactsFakeData.contact1Email,
                AddressLine = "123 Emet St",
                City = "New York",
                State = "New York",
                ProfileImage = null,
                BirthDate = new DateTime(2001, 1, 1),
                PhoneNumbers = new List<BasePhoneNumber>()
                {
                    new BasePhoneNumber
                    {
                        Type = 2,
                        Number = RandomNumberGenerator.GenerateRandomNumber()
                    }
                }
            };
            var expectedStatus = StatusCodes.Status400BadRequest;

            // act

            var res = (await this.Controller.Create(model)).Result;
            var response = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).Value;
            var actualStatus = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).StatusCode;

            // assert

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal(ContactManager.EmailError, ((LaNacion.Entities.Api.Version1.ErrorResponse)response).ErrorMessage);
        }

        [Fact]
        public async Task DuplicatePhone_ReturnBadRequest()
        {
            // arrange

            var model = new CreateContactRequest()
            {
                Name = "Joe Washington",
                Company = "ABC Company",
                Email = ContactsFakeData.contact1Email,
                AddressLine = "123 Emet St",
                City = "New York",
                State = "New York",
                ProfileImage = null,
                BirthDate = new DateTime(2001, 1, 1),
                PhoneNumbers = new List<BasePhoneNumber>()
                {
                    new BasePhoneNumber
                    {
                        Type = 2,
                        Number = ContactsFakeData.contact1PhoneNumber
                    }
                }
            };
            var expectedStatus = StatusCodes.Status400BadRequest;

            // act

            var res = (await this.Controller.Create(model)).Result;
            var response = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).Value;
            var actualStatus = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).StatusCode;

            // assert

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal(ContactManager.PhoneNumberError, ((LaNacion.Entities.Api.Version1.ErrorResponse)response).ErrorMessage);
        }
    }
}