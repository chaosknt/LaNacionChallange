using System;

namespace LaNacion.Common
{
    public static class RandomNumberGenerator
    {
        public static string GenerateRandomNumber()
        {
            Random random = new Random();
            return random.Next(1000, 99999).ToString() + "-" + random.Next(1000, 99999).ToString();
        }
    }
}
