namespace LaNacion.Entities.Api.Version1
{
    public class ErrorResponse : BasicResponse
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }
    }
}
