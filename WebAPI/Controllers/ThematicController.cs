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
    public class ThematicController : DefaultController
    {
        private readonly string secret;
        private readonly ThematicService thematicService;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public ThematicController(IConfiguration conf, ThematicService thematicService, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.thematicService = thematicService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ListThematic()
        {
            return Ok(await thematicService.ListThematic());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetThematic([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await thematicService.GetThematic(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddThematic([FromBody] Thematic thematic)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await thematicService.AddThematic(thematic));
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateThematic([FromBody] Thematic thematic)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await thematicService.UpdateThematic(thematic));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveThematic([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            await thematicService.RemoveThematic(id);

            return Ok();
        }


    }
}
