using System.Text.RegularExpressions;

namespace LaNacion.Common.Helpers.Email
{
    public static class EmailHelper
    {
        private const string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        public static bool IsValidEmail(string val)
        {
            return Regex.IsMatch(val, emailPattern);
        }
    }
}
