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
    public class ActivityController : DefaultController
    {
        private readonly string secret;
        private readonly ActivityService activityService;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public ActivityController(IConfiguration conf, ActivityService activityService, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.activityService = activityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ListActivity()
        {
            return Ok(await activityService.ListActivity());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetActivity([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await activityService.GetActivity(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddActivity([FromBody] Activity activity)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await activityService.AddActivity(activity));
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateActivity([FromBody] Activity activity)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            return Ok(await activityService.UpdateActivity(activity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveActivity([FromRoute] int id)
        {
            Structure structure = await GetAuthenticatedStructure();
            User user = await GetAuthenticatedUser();
            if (!IsAuthorizedStructure(structure) && !IsAuthorizedUser(user))
                return Forbid();

            await activityService.RemoveActivity(id);

            return Ok();
        }


    }
}
