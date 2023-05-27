using LaNacion.Common.Enum;
using LaNacion.Common.Extensions;
using LaNacion.Entities.Api;
using LaNacion.Entities.Api.Version1;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace LaNacion.Api.Controllers.Api.Version1
{
    public class BaseApiController : ControllerBase
    {
        internal readonly ILogger logger;

        public BaseApiController(ILogger logger)
        {
            this.logger = logger;
        }

        internal ActionResult<BasicResponse> InvalidPayloadResponse(string errorMessage, Guid correlationId)
        {
            return this.BadRequest(new ErrorResponse { Status = RequestStatus.Error.GetFriendlyName(), ErrorMessage = errorMessage, ErrorCode = KnownErrorCodesEnum.InvalidPayload.AsInt(), CorrelationId = correlationId });
        }

        internal ActionResult<BasicResponse> UnauthorizedResponse(Guid correlationId)
        {
            return this.Unauthorized(new BasicResponse { Status = RequestStatus.Unauthorized.GetFriendlyName(), CorrelationId = correlationId });
        }

        internal ActionResult<BasicResponse> ErrorResponse(Exception exception, Guid correlationId, ILogger loggerContext)
            => this.ErrorResponse(exception, exception.Message, correlationId, loggerContext);

        internal ActionResult<BasicResponse> ErrorResponse(Exception exception, string errorMessage, Guid correlationId, ILogger loggerContext)
        {
            loggerContext = loggerContext.ForContext("StackTrace", exception.StackTrace);
            loggerContext = loggerContext.ForContext("ErrorMessage", exception.Message);
            if (exception.InnerException != null)
            {
                loggerContext = loggerContext.ForContext("InnerException", exception.InnerException.Message);
            }

            loggerContext.Error(exception, errorMessage);

            return this.BadRequest(new ErrorResponse { Status = RequestStatus.Error.GetFriendlyName(), ErrorMessage = errorMessage, ErrorCode = KnownErrorCodesEnum.UnknownError.AsInt(), CorrelationId = correlationId });
        }

        internal ActionResult<BasicResponse> FailedResponse(Exception exception, KnownErrorCodesEnum errorCode, Guid correlationId, ILogger loggerContext)
            => this.FailedResponse(exception, exception.Message, errorCode, correlationId, loggerContext);

        internal ActionResult<BasicResponse> FailedResponse(Exception exception, string errorMessage, KnownErrorCodesEnum errorCode, Guid correlationId, ILogger loggerContext)
        {
            loggerContext.Error(exception, errorMessage);

            return this.BadRequest(new ErrorResponse { Status = RequestStatus.Failed.GetFriendlyName(), ErrorMessage = errorMessage, ErrorCode = errorCode.AsInt(), CorrelationId = correlationId });
        }
    }
}