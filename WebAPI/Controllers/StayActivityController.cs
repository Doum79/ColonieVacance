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
    public class StayActivityController : DefaultController
    {
        private readonly string secret;
        private readonly StayActivityService stayActivityService;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public StayActivityController(IConfiguration conf, StayActivityService stayActivityService, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.stayActivityService = stayActivityService;
        }

        [AllowAnonymous]
        [HttpGet("stay/{stayId}")]
        public async Task<ActionResult> ListActivity([FromRoute] int stayId)
        {
            return Ok(await stayActivityService.ListActivityOfStay(stayId));
        }
    }
}
