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
    public class StructureController : DefaultController
    {
        private readonly string secret;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public StructureController(IConfiguration conf, StructureService structureService, UserService userService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.userService = userService;
            this.structureService = structureService;
        }

        [HttpGet("{structureId}")]
        public async Task<IActionResult> GetStructure([FromRoute] int? structureId)
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            return Ok(await structureService.GetStructureById(structureId.Value));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStructure([FromBody] Structure uStructure)
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            var sendStructure = await structureService.UpdateStructure(uStructure);
            sendStructure.Password = null;

            return Ok(sendStructure);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ListStructures()
        {
            return Ok(await structureService.ListStructure());
        }

        [HttpDelete("{structureId}")]
        public async Task<IActionResult> DeleteStructure([FromRoute] int? structureId)
        {
            Structure structure = await GetAuthenticatedStructure();
            if (!IsAuthorizedStructure(structure))
                return Forbid();

            await structureService.DeleteStructure(structureId.Value);

            return Ok();
        }

        [HttpPost("favorite/{structureId}")]
        public async Task<IActionResult> AddFavoriteStructure([FromRoute] int structureId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            await structureService.AddFavoriteStructure(structureId, user.Id);

            return Ok();
        }

        [HttpDelete("favorite/{structureId}")]
        public async Task<IActionResult> DeleteFavoriteStructure([FromRoute] int structureId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            await structureService.DeleteFavoriteStructure(structureId, user.Id);

            return Ok();
        }

        [HttpGet("favorite")]
        public async Task<IActionResult> FavoriteStructuresList([FromRoute] int stayId)
        {
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedUser(user))
                return Forbid();

            return Ok(await structureService.FavoriteStructuresListByUser(user.Id));
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] Structure newStructure)
        {
            if (newStructure == null)
                return BadRequest();

            return Ok(await structureService.Registration(newStructure));
        }
    }
}
