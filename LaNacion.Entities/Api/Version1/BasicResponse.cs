using System;

namespace LaNacion.Entities.Api.Version1
{
    public class BasicResponse
    {
        public string Status { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
