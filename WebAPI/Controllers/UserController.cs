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
    [ApiController]
    [Route("[controller]")]
    public class UserController : DefaultController
    {
        private readonly string secret;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public UserController(IConfiguration conf, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int? userId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            return Ok(await userService.GetUserById(userId.Value));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromBody] User uUser)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            var sendUser = await userService.UpdateUser(uUser);
            sendUser.Password = null;

            return Ok(sendUser);
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            return Ok(await userService.ListUsers());
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int? userId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            await userService.DeleteUser(userId.Value);

            return Ok();
        }


        [HttpPost("favoriteStructure/{structureId}")]
        public async Task<IActionResult> AddFavoriteStay([FromRoute] int strucutreId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            return Ok(await structureService.AddFavoriteStructure(strucutreId, user.Id));
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] User newUser)
        {
            if (newUser == null)
                return BadRequest();

            return Ok(await userService.Registration(newUser));
        }
    }
}
