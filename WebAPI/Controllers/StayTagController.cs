using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StayTagController : DefaultController
    {
        private readonly string secret;
        private readonly StayTagService stayTagService;
        private readonly UserService userService;
        private readonly StructureService structureService;

        public StayTagController(IConfiguration conf, StayTagService stayTagService, UserService userService, StructureService structureService) : base(userService, structureService)
        {
            secret = conf["ApplicationSettings:Secret"];
            this.stayTagService = stayTagService;
        }

        [AllowAnonymous]
        [HttpGet("stay/{stayId}")]
        public async Task<ActionResult> ListTag([FromRoute] int stayId)
        {
            return Ok(await stayTagService.ListTagOfStay(stayId));
        }
    }
}
