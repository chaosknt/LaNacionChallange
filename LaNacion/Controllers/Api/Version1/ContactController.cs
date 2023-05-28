using LaNacion.Api.Mappers;
using LaNacion.Common.Extensions;
using LaNacion.Common.Helpers.Email;
using LaNacion.Common.Search;
using LaNacion.Data.Models;
using LaNacion.Data.Service.Exceptions;
using LaNacion.Data.Service.Managers;
using LaNacion.Entities.Api;
using LaNacion.Entities.Api.Version1;
using LaNacion.Entities.Api.Version1.Contacts;
using LaNacion.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaNacion.Api.Controllers.Api.Version1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContactController : BaseApiController
    {
        private readonly IContactManager contactManager;

        public ContactController(
                           ILogger logger,
                           IContactManager contactManager)
           : base(logger)
        {
            this.contactManager = contactManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("create")]
        public async Task<ActionResult<BasicResponse>> Create([FromForm] CreateContactRequest request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                var result = await this.contactManager.CreateAsync(request);
                return this.StatusCode(StatusCodes.Status201Created, new CreateContactResponse { ContactId = result, Status = RequestStatus.Accepted.GetFriendlyName(), CorrelationId = correlationId });
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Create Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("get/{id}")]
        public async Task<ActionResult<BasicResponse>> Get(Guid id, Guid? correlation)
        {
            var correlationId = correlation ?? Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(id));

            try
            {
                var contact = (await this.contactManager.GetByIdAsync(id)).ToResponseModel();
                return base.Ok(new Entities.Api.Version1.Contacts.SearchResult<ContactResponse>()
                {
                    Status = RequestStatus.Ok.GetFriendlyName(),
                    CorrelationId = correlationId,
                    Result = contact,
                });
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Get Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("update/{id}")]
        public async Task<ActionResult<BasicResponse>> Update([FromForm] UpdateContactRequest request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                await this.contactManager.UpdateAsync(request);
                return await this.Get(request.Id, correlationId);
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Update Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("delete/{id}")]
        public async Task<ActionResult<BasicResponse>> Delete(Guid id)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(id));

            try
            {
                await this.contactManager.DeleteAsync(id);
                return Ok(new BasicResponse { Status = RequestStatus.Accepted.GetFriendlyName(), CorrelationId = correlationId });
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Delete Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("search")]
        public async Task<ActionResult<BasicResponse>> Search(string value)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(value));

            try
            {
                return base.Ok(new Entities.Api.Version1.Contacts.SearchResult<ContactResponse>()
                {
                    Status = RequestStatus.Ok.GetFriendlyName(),
                    CorrelationId = correlationId,
                    Result = (await this.contactManager.SearchAsync(new SearchFilter(EmailHelper.IsValidEmail(value) ? nameof(ContactDbEntity.Email) : nameof(PhoneNumberDbEntity.Number), value))).ToResponseModel(),
                });
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Search Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("find")]
        public async Task<ActionResult<BasicResponse>> FindByLocation([FromQuery] FindByLocationRequest request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                var paramName = request.Type == Entities.Enums.LocationType.City ? nameof(ContactDbEntity.City) : nameof(ContactDbEntity.State);
                var result = await this.contactManager.FindByLocationAsync(new SearchFilter(paramName, request.SearchedValue, request.Start, request.Length, request.Order));
                return base.Ok(new Entities.Api.Version1.Contacts.SearchResult<IEnumerable<ContactResponse>>()
                {
                    Status = RequestStatus.Ok.GetFriendlyName(),
                    CorrelationId = correlationId,
                    ItemsTotal = result.ItemsTotal,
                    Result = result.Items.Select(x => x.ToResponseModel())
                });
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the FindByLocation Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("enum")]
        public ActionResult EnumValues()
        {
            var correlationId = Guid.NewGuid();
            return base.Ok(new
            {
                correlationId,
                Locations = EnumExtensions.ToDisplayValueDictionary<LocationType>(),
                NumberTypes = EnumExtensions.ToDisplayValueDictionary<NumberTypes>(),
                SortOrders = EnumExtensions.ToDisplayValueDictionary<SortOrder>(),
            });
        }
    }
}