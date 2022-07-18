using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly string secret;
        private readonly UserService userService;
        private readonly StructureService structureService;
        private readonly HashPasswordService hashPasswordService;
        private readonly StayService stayService;

        public AuthenticationController(IConfiguration conf, UserService userService,
            HashPasswordService hashPasswordService, StructureService structureService, StayService stayService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.userService = userService;
            this.hashPasswordService = hashPasswordService;
            this.structureService = structureService;
            this.stayService = stayService;
        }

        [AllowAnonymous]
        [HttpPost("connexion")]
        public async Task<IActionResult> UserConnexion([FromBody] DTO.LoginInfo login)
        {

            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest();

            var user = await userService.GetUserByEmail(login.Email);
            var structure = await structureService.GetStructureByEmail(login.Email);

            if (user == null && structure == null)
                throw new Model.Exceptions.EmailNotExistException();

            var checkPassword = hashPasswordService.SetHashPassword(login.Password);

            

            if(user != null)
            {
                if (user.Password != checkPassword)
                    throw new Model.Exceptions.InvalidPasswordException();

                var token = this.CreateToken(user.Email);
                user.Password = null;
                return Ok(new { token, user });
            }
            else
            {
                if (structure.Password != checkPassword)
                    throw new Model.Exceptions.InvalidPasswordException();

                var token = this.CreateToken(structure.Email);
                structure.Password = null;
                return Ok(new { token, structure });
            }
        }

        [AllowAnonymous]
        [HttpGet("view")]
        public async Task<IActionResult> AddViewToStay([FromQuery] int stayId)
        {
            await stayService.AddView(stayId);
            return Ok();
        }

        private string CreateToken(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmail)
                }),
                Expires = DateTime.UtcNow.AddMinutes(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
