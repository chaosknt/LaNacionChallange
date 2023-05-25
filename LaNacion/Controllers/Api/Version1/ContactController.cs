using LaNacion.Data.Service.Exceptions;
using LaNacion.Entities.Api.Version1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading.Tasks;

namespace LaNacion.Api.Controllers.Api.Version1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContactController : BaseApiController
    {
        public ContactController(
                           ILogger logger)
           : base(logger)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("create")]
        public async Task<ActionResult<BasicResponse>> Create(string request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                return null;
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Check In Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("get/{id}")]
        public async Task<ActionResult<BasicResponse>> Get(string request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                return null;
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Check In Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("update/{id}")]
        public async Task<ActionResult<BasicResponse>> Update(string request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                return null;
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Check In Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("delete/{id}")]
        public async Task<ActionResult<BasicResponse>> Delete(string request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                return null;
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Check In Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("search")]
        public async Task<ActionResult<BasicResponse>> Search(string request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                return null;
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Check In Request, try again later", correlationId, loggerContext);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("find")]
        public async Task<ActionResult<BasicResponse>> FindByLocation(string request)
        {
            var correlationId = Guid.NewGuid();
            var loggerContext = this.logger
                .ForContext("CorrelationId", correlationId)
                .ForContext("RequestBody", JsonConvert.SerializeObject(request));

            try
            {
                return null;
            }
            catch (BussinessException ex)
            {
                return base.FailedResponse(ex, ex.ErrorCode, correlationId, loggerContext);
            }
            catch (Exception ex)
            {
                return base.ErrorResponse(ex, "There was an error processing the Check In Request, try again later", correlationId, loggerContext);
            }
        }
    }
}
