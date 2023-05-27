using LaNacion.Common.Enum;
using System;

namespace LaNacion.Data.Service.Exceptions
{
    public class BussinessException : Exception
    {
        public BussinessException(KnownErrorCodesEnum errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public BussinessException(string message, KnownErrorCodesEnum errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BussinessException(string message, Exception innerException, KnownErrorCodesEnum errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public KnownErrorCodesEnum ErrorCode { get; set; }
    }
}
