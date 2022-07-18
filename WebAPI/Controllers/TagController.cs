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
    public class TagController : DefaultController
    {
        private readonly string secret;
        private readonly TagService tagService;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public TagController(IConfiguration conf, TagService tagService, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.tagService = tagService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ListTag()
        {
            return Ok(await tagService.ListTag());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTag([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await tagService.GetTag(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddTag([FromBody] Tag tag)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await tagService.AddTag(tag));
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateTag([FromBody] Tag tag)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await tagService.UpdateTag(tag));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTag([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            await tagService.RemoveTag(id);
            return Ok();
        }


    }
}
