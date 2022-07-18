using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class HashPasswordService
    {
        public string SetHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 1,
            numBytesRequested: 512 / 8));

            return hashedPassword;
        }
    }
}
