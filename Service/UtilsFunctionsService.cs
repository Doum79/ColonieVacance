using System;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using Model;
using Model.Exceptions;

namespace Service
{
    public class UtilsFunctionsService
    {
        public static string SetHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1,
                numBytesRequested: 512 / 8
                ));
        }

        public static string RandomString(int length)
        {
             Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int DiscountPercentage(double number1, double number2)
        {
            var value = Convert.ToInt32(number2 / number1 * 100);
            return 100 - value;
        }

        public static void IsStructure(dynamic currentUser)
        {
            if (currentUser["profil"] != Structure.PROFIL_STRUCTURE)
                throw new UnauthoriseException();
        }

        public static void IsParent(dynamic currentUser)
        {
            if (currentUser["profil"] != User.PROFIL_PARENT)
                throw new UnauthoriseException();
        }
    }
}
